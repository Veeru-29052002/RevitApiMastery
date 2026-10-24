using RevitApiMastery.Models;

namespace RevitApiMastery.Services;

public class AuthService
{
    private static readonly List<UserAccount> SeedUsers = new()
    {
        new UserAccount
        {
            Id = "usr-admin-01",
            Username = "Veeru@2002",
            FullName = "Veera Babu Barla (Lead Academy Administrator)",
            Email = "bveerababu612@gmail.com",
            Password = "Admin@2002",
            Role = UserRole.Admin,
            Title = "Chief Academy Administrator & Enterprise BIM Architect",
            Organization = "Autodesk Revit API Master Academy",
            AvatarColor = "#A855F7",
            EnrolledDate = DateTime.UtcNow.AddYears(-1)
        },
        new UserAccount
        {
            Id = "usr-student-01",
            Username = "alex.rivera",
            FullName = "Alex Rivera, EIT",
            Email = "student@revitapi.com",
            Password = "user123",
            Role = UserRole.User,
            Title = "BIM Computational Engineer",
            Organization = "Foster Architecture Studio",
            AvatarColor = "#10B981",
            EnrolledDate = DateTime.UtcNow.AddMonths(-3)
        }
    };

    private static readonly Dictionary<string, string> ResetTokens = new(StringComparer.OrdinalIgnoreCase);

    public UserAccount? CurrentUser { get; private set; }

    public event Action? OnAuthStateChanged;

    public bool IsAuthenticated => CurrentUser != null;
    public bool IsAdmin => CurrentUser?.Role == UserRole.Admin;
    public bool IsUser => CurrentUser?.Role == UserRole.User;

    private readonly EmailService? _emailService;

    public AuthService(EmailService? emailService = null)
    {
        _emailService = emailService;
    }

    public (bool Success, string? ErrorMessage) Login(string emailOrUsername, string password, UserRole targetRole)
    {
        if (string.IsNullOrWhiteSpace(emailOrUsername) || string.IsNullOrWhiteSpace(password))
        {
            return (false, "Please provide both email/username and password.");
        }

        var normalized = emailOrUsername.Trim();
        var cleanPass = password.Trim();

        lock (SeedUsers)
        {
            // Match by username, email, or admin aliases
            var user = SeedUsers.FirstOrDefault(u => 
                string.Equals(u.Username, normalized, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(u.Email, normalized, StringComparison.OrdinalIgnoreCase) ||
                (u.Role == UserRole.Admin && (
                    normalized.Equals("bveerababu612@gmail.com", StringComparison.OrdinalIgnoreCase) ||
                    normalized.Equals("Veeru@2002", StringComparison.OrdinalIgnoreCase) ||
                    normalized.Equals("veeru", StringComparison.OrdinalIgnoreCase) ||
                    normalized.Equals("admin", StringComparison.OrdinalIgnoreCase) ||
                    normalized.Equals("admin@revitapi.com", StringComparison.OrdinalIgnoreCase)
                )) ||
                (u.Role == UserRole.User && normalized.Equals("student", StringComparison.OrdinalIgnoreCase)));

            if (user == null)
            {
                return (false, $"No account registered matching '{emailOrUsername}'.");
            }

            // Verify password (allow case-insensitive match for convenience)
            if (!string.Equals(user.Password, cleanPass, StringComparison.Ordinal) &&
                !string.Equals(user.Password, cleanPass, StringComparison.OrdinalIgnoreCase))
            {
                return (false, "Incorrect password. Please verify credentials.");
            }

            CurrentUser = user;
        }

        NotifyStateChanged();
        return (true, null);
    }

    public (bool Success, string? ErrorMessage) UpdateProfile(
        string userId,
        string fullName,
        string email,
        string username,
        string title,
        string organization)
    {
        lock (SeedUsers)
        {
            var user = SeedUsers.FirstOrDefault(u => u.Id == userId);
            if (user == null) return (false, "User account not found.");

            if (string.IsNullOrWhiteSpace(fullName)) return (false, "Full Name is required.");
            if (string.IsNullOrWhiteSpace(email)) return (false, "Email is required.");
            if (string.IsNullOrWhiteSpace(username)) return (false, "Username is required.");

            var normEmail = email.Trim();
            var normUsername = username.Trim();

            // Conflict check
            var conflict = SeedUsers.FirstOrDefault(u => u.Id != userId && 
                (string.Equals(u.Email, normEmail, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(u.Username, normUsername, StringComparison.OrdinalIgnoreCase)));

            if (conflict != null)
            {
                return (false, "That email or username is already in use by another account.");
            }

            user.FullName = fullName.Trim();
            user.Email = normEmail;
            user.Username = normUsername;
            user.Title = string.IsNullOrWhiteSpace(title) ? user.Title : title.Trim();
            user.Organization = string.IsNullOrWhiteSpace(organization) ? user.Organization : organization.Trim();

            if (CurrentUser?.Id == userId)
            {
                CurrentUser = user;
            }
        }

        NotifyStateChanged();
        return (true, null);
    }

    public (bool Success, string? ErrorMessage) ChangePassword(string userId, string currentPassword, string newPassword)
    {
        lock (SeedUsers)
        {
            var user = SeedUsers.FirstOrDefault(u => u.Id == userId);
            if (user == null) return (false, "User account not found.");

            if (!string.Equals(user.Password, currentPassword, StringComparison.Ordinal) &&
                !string.Equals(user.Password, currentPassword, StringComparison.OrdinalIgnoreCase))
            {
                return (false, "Current password does not match.");
            }

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 4)
            {
                return (false, "New password must be at least 4 characters long.");
            }

            user.Password = newPassword.Trim();
        }

        NotifyStateChanged();
        return (true, null);
    }

    public (bool Success, string? ErrorMessage, string? ResetToken) RequestAdminPasswordReset(string email, string? resetUrl = null)
    {
        if (string.IsNullOrWhiteSpace(email)) return (false, "Please provide your admin email.", null);

        var norm = email.Trim();
        lock (SeedUsers)
        {
            var admin = SeedUsers.FirstOrDefault(u => u.Role == UserRole.Admin && 
                (string.Equals(u.Email, norm, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(u.Username, norm, StringComparison.OrdinalIgnoreCase) ||
                 norm.Equals("bveerababu612@gmail.com", StringComparison.OrdinalIgnoreCase) ||
                 norm.Equals("Veeru@2002", StringComparison.OrdinalIgnoreCase)));

            if (admin == null)
            {
                return (false, $"No Administrator account found associated with '{email}'.", null);
            }

            var token = "RST-" + new Random().Next(100000, 999999);
            ResetTokens[admin.Email] = token;

            // Dispatch reset verification email to Admin (bveerababu612@gmail.com)
            if (_emailService != null)
            {
                var resolvedUrl = !string.IsNullOrWhiteSpace(resetUrl) ? resetUrl : "http://localhost:5078/login";
                _ = _emailService.SendAdminPasswordResetEmailAsync(admin.Email, token, resolvedUrl);
            }

            return (true, null, token);
        }
    }

    public (bool Success, string? ErrorMessage) ResetPasswordWithToken(string email, string token, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 4)
        {
            return (false, "New password must be at least 4 characters.");
        }

        var norm = email.Trim();
        lock (SeedUsers)
        {
            var admin = SeedUsers.FirstOrDefault(u => u.Role == UserRole.Admin && 
                (string.Equals(u.Email, norm, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(u.Username, norm, StringComparison.OrdinalIgnoreCase) ||
                 norm.Equals("bveerababu612@gmail.com", StringComparison.OrdinalIgnoreCase) ||
                 norm.Equals("Veeru@2002", StringComparison.OrdinalIgnoreCase)));

            if (admin == null) return (false, "Admin account not found.");

            if (!ResetTokens.TryGetValue(admin.Email, out var validToken) || !string.Equals(validToken, token.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return (false, "Invalid or expired password reset token.");
            }

            admin.Password = newPassword.Trim();
            ResetTokens.Remove(admin.Email);
        }

        NotifyStateChanged();
        return (true, null);
    }

    public void QuickLogin(UserRole role)
    {
        lock (SeedUsers)
        {
            var user = SeedUsers.FirstOrDefault(u => u.Role == role);
            if (user != null)
            {
                CurrentUser = user;
            }
        }
        NotifyStateChanged();
    }

    public void QuickLoginAsUser(string userId)
    {
        lock (SeedUsers)
        {
            var user = SeedUsers.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                CurrentUser = user;
            }
        }
        NotifyStateChanged();
    }

    public (bool Success, string? ErrorMessage, UserAccount? Account) EnrollUser(
        string fullName,
        string usernameOrEmail,
        string password,
        UserRole role,
        string title,
        string organization,
        string? portalUrl = null)
    {
        lock (SeedUsers)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return (false, "Full Name is required.", null);
            if (string.IsNullOrWhiteSpace(usernameOrEmail))
                return (false, "Username or Email is required.", null);
            if (string.IsNullOrWhiteSpace(password))
                return (false, "Password is required.", null);

            var normalized = usernameOrEmail.Trim();
            if (SeedUsers.Any(u => string.Equals(u.Email, normalized, StringComparison.OrdinalIgnoreCase) || 
                                   string.Equals(u.Username, normalized, StringComparison.OrdinalIgnoreCase)))
            {
                return (false, $"An account with username or email '{usernameOrEmail}' already exists.", null);
            }

            var newAccount = new UserAccount
            {
                Id = "usr-" + Guid.NewGuid().ToString("N")[..8],
                Username = normalized,
                Email = normalized.Contains('@') ? normalized : $"{normalized.ToLowerInvariant()}@revitapi.com",
                FullName = fullName.Trim(),
                Password = password.Trim(),
                Role = role,
                Title = string.IsNullOrWhiteSpace(title) ? "BIM Developer / Engineer" : title.Trim(),
                Organization = string.IsNullOrWhiteSpace(organization) ? "Independent AEC Studio" : organization.Trim(),
                AvatarColor = role == UserRole.Admin ? "#A855F7" : "#00EBFF",
                EnrolledDate = DateTime.UtcNow
            };

            SeedUsers.Insert(1, newAccount);

            // Automatically dispatch login credentials email directly to the newly enrolled user!
            if (_emailService != null)
            {
                var resolvedUrl = !string.IsNullOrWhiteSpace(portalUrl) ? portalUrl : "http://localhost:5078/login";
                _ = _emailService.SendUserCredentialsEmailAsync(
                    newAccount.Email,
                    newAccount.FullName,
                    newAccount.Username,
                    newAccount.Password,
                    newAccount.Role.ToString(),
                    resolvedUrl
                );
            }

            NotifyStateChanged();
            return (true, null, newAccount);
        }
    }

    public bool DeleteUser(string userId)
    {
        lock (SeedUsers)
        {
            var u = SeedUsers.FirstOrDefault(x => x.Id == userId);
            if (u != null && 
                !string.Equals(u.Username, "Veeru@2002", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(u.Email, "bveerababu612@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                SeedUsers.Remove(u);
                NotifyStateChanged();
                return true;
            }
            return false;
        }
    }

    public void Logout()
    {
        CurrentUser = null;
        NotifyStateChanged();
    }

    public IReadOnlyList<UserAccount> GetRegisteredUsers()
    {
        lock (SeedUsers)
        {
            return SeedUsers.ToList();
        }
    }

    private void NotifyStateChanged()
    {
        OnAuthStateChanged?.Invoke();
    }
}
