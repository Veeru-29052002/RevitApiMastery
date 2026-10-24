namespace RevitApiMastery.Models;

public enum UserRole
{
    User,   // Student / BIM Developer
    Admin   // Instructor / Academy Administrator
}

public class UserAccount
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public string Title { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string AvatarColor { get; set; } = "#00EBFF";
    public DateTime EnrolledDate { get; set; } = DateTime.UtcNow.AddMonths(-2);
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole TargetRole { get; set; } = UserRole.User;
    public bool RememberMe { get; set; } = true;
}
