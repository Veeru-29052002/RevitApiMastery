using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RevitApiMastery.Models;

namespace RevitApiMastery.Services;

public class EmailService
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment? _env;
    private readonly List<EmailMessage> _outbox = new();
    
    public string AdminEmail { get; set; } = "bveerababu612@gmail.com";
    public string SmtpHost { get; set; } = "smtp.gmail.com";
    public int SmtpPort { get; set; } = 587;
    public bool EnableSsl { get; set; } = true;
    public string SenderEmail { get; set; } = "bveerababu612@gmail.com";
    public string SenderName { get; set; } = "Autodesk Revit API & C# Master Academy";
    public string SmtpUsername { get; set; } = "bveerababu612@gmail.com";
    public string SmtpPassword { get; set; } = "";

    public bool IsSmtpConfigured => !string.IsNullOrWhiteSpace(SmtpPassword);

    public event Action? OnEmailDispatched;

    public EmailService(IConfiguration config, IWebHostEnvironment? env = null)
    {
        _config = config;
        _env = env;
        
        // Read configuration if present
        var adminEmail = _config["AdminSettings:AdminEmail"];
        if (!string.IsNullOrWhiteSpace(adminEmail))
        {
            AdminEmail = adminEmail.Trim();
            SenderEmail = adminEmail.Trim();
            SmtpUsername = adminEmail.Trim();
        }

        var host = _config["SmtpSettings:Host"];
        if (!string.IsNullOrWhiteSpace(host)) SmtpHost = host;

        if (int.TryParse(_config["SmtpSettings:Port"], out var port)) SmtpPort = port;
        if (bool.TryParse(_config["SmtpSettings:EnableSsl"], out var ssl)) EnableSsl = ssl;

        var pass = _config["SmtpSettings:Password"];
        if (!string.IsNullOrWhiteSpace(pass)) SmtpPassword = pass.Trim().Replace(" ", "");
    }

    public async Task<bool> SaveSmtpPasswordAsync(string password, string? host = null, int? port = null, string? username = null)
    {
        SmtpPassword = password.Trim().Replace(" ", "");
        if (!string.IsNullOrWhiteSpace(host)) SmtpHost = host.Trim();
        if (port.HasValue && port.Value > 0) SmtpPort = port.Value;
        if (!string.IsNullOrWhiteSpace(username))
        {
            SmtpUsername = username.Trim();
            SenderEmail = username.Trim();
        }

        try
        {
            var contentRoot = _env?.ContentRootPath ?? Directory.GetCurrentDirectory();
            var jsonPath = Path.Combine(contentRoot, "appsettings.json");
            if (File.Exists(jsonPath))
            {
                var jsonStr = await File.ReadAllTextAsync(jsonPath);
                var rootNode = System.Text.Json.Nodes.JsonNode.Parse(jsonStr);
                if (rootNode != null)
                {
                    if (rootNode["SmtpSettings"] == null)
                    {
                        rootNode["SmtpSettings"] = new System.Text.Json.Nodes.JsonObject();
                    }
                    rootNode["SmtpSettings"]!["Password"] = SmtpPassword;
                    if (!string.IsNullOrWhiteSpace(host)) rootNode["SmtpSettings"]!["Host"] = SmtpHost;
                    if (port.HasValue) rootNode["SmtpSettings"]!["Port"] = SmtpPort.ToString();
                    if (!string.IsNullOrWhiteSpace(username)) rootNode["SmtpSettings"]!["Username"] = SmtpUsername;

                    var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
                    await File.WriteAllTextAsync(jsonPath, rootNode.ToJsonString(options));
                }
            }
            OnEmailDispatched?.Invoke();
            return true;
        }
        catch
        {
            OnEmailDispatched?.Invoke();
            return false;
        }
    }

    public async Task<(bool success, string status, string? error)> ResendEmailAsync(EmailMessage email)
    {
        if (string.IsNullOrWhiteSpace(SmtpPassword))
        {
            email.IsSuccess = false;
            email.Status = "Pending Google App Password";
            email.ErrorMessage = "Please configure a Google App Password in SMTP Settings first.";
            OnEmailDispatched?.Invoke();
            return (false, email.Status, email.ErrorMessage);
        }

        try
        {
            using var client = new SmtpClient(SmtpHost, SmtpPort)
            {
                EnableSsl = EnableSsl,
                Credentials = new NetworkCredential(SmtpUsername, SmtpPassword),
                Timeout = 12000
            };

            using var mailMsg = new MailMessage
            {
                From = new MailAddress(SenderEmail, SenderName),
                Subject = email.Subject,
                Body = email.BodyHtml,
                IsBodyHtml = true
            };

            mailMsg.To.Add(new MailAddress(email.RecipientEmail, email.RecipientName));

            await client.SendMailAsync(mailMsg);
            email.SentAt = DateTime.UtcNow;
            email.IsSuccess = true;
            email.Status = "Delivered via Live SMTP";
            email.ErrorMessage = null;
            OnEmailDispatched?.Invoke();
            return (true, email.Status, null);
        }
        catch (Exception ex)
        {
            email.SentAt = DateTime.UtcNow;
            email.IsSuccess = false;
            email.Status = "SMTP Transmission Error";
            email.ErrorMessage = ex.Message;
            OnEmailDispatched?.Invoke();
            return (false, email.Status, ex.Message);
        }
    }

    public IReadOnlyList<EmailMessage> GetOutbox()
    {
        lock (_outbox)
        {
            return _outbox.OrderByDescending(e => e.SentAt).ToList();
        }
    }

    public async Task<EmailMessage> SendUserCredentialsEmailAsync(
        string recipientEmail, 
        string recipientName, 
        string username, 
        string password, 
        string role, 
        string portalUrl)
    {
        var subject = $"Welcome to Revit API & C# Master Academy - Your Login Credentials";
        var bodyHtml = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'/>
    <style>
        body {{ font-family: 'Segoe UI', Helvetica, Arial, sans-serif; background-color: #0b111e; color: #f8fafc; padding: 20px; margin: 0; }}
        .card {{ background-color: #10192a; border: 1px solid #00f0ff; border-radius: 8px; max-width: 600px; margin: 0 auto; padding: 30px; box-shadow: 0 10px 30px rgba(0,0,0,0.5); }}
        .header {{ border-bottom: 1px solid rgba(0,240,255,0.2); padding-bottom: 15px; margin-bottom: 20px; }}
        .title {{ color: #00f0ff; font-size: 20px; font-weight: bold; margin: 0; }}
        .subtitle {{ color: #94a3b8; font-size: 13px; margin-top: 4px; }}
        .cred-box {{ background: #070b14; border: 1px solid rgba(255,255,255,0.1); border-left: 4px solid #00f0ff; padding: 18px; border-radius: 6px; margin: 20px 0; }}
        .cred-item {{ font-size: 14px; margin-bottom: 10px; }}
        .cred-label {{ color: #94a3b8; width: 110px; display: inline-block; font-family: monospace; font-size: 12px; }}
        .cred-val {{ color: #00f0ff; font-weight: bold; font-family: monospace; font-size: 14px; background: rgba(0,240,255,0.08); padding: 2px 8px; border-radius: 4px; }}
        .btn {{ display: inline-block; background: linear-gradient(135deg, #0078d4, #00f0ff); color: #050811 !important; font-weight: bold; padding: 12px 24px; text-decoration: none; border-radius: 6px; margin-top: 15px; }}
        .footer {{ font-size: 12px; color: #64748b; margin-top: 25px; border-top: 1px solid rgba(255,255,255,0.08); padding-top: 15px; }}
    </style>
</head>
<body>
    <div class='card'>
        <div class='header'>
            <div class='title'>AUTODESK REVIT API & MODERN C# ACADEMY</div>
            <div class='subtitle'>Official Student Developer Enrollment Notice</div>
        </div>

        <p>Hello <strong>{recipientName}</strong>,</p>
        <p>Congratulations! You have been officially enrolled by the Academy Administrator into the <strong>Revit API & C# Engineering Master Academy</strong>.</p>
        <p>Your dedicated workstation credentials have been generated and issued below:</p>

        <div class='cred-box'>
            <div class='cred-item'>
                <span class='cred-label'>ACCESS LEVEL:</span>
                <span style='color: #4ade80; font-weight: bold;'>{role.ToUpperInvariant()}</span>
            </div>
            <div class='cred-item'>
                <span class='cred-label'>USERNAME:</span>
                <span class='cred-val'>{username}</span>
            </div>
            <div class='cred-item'>
                <span class='cred-label'>PASSWORD:</span>
                <span class='cred-val'>{password}</span>
            </div>
            <div class='cred-item'>
                <span class='cred-label'>LOGIN URL:</span>
                <span style='color: #cbd5e1; font-family: monospace; font-size: 12px;'>{portalUrl}</span>
            </div>
        </div>

        <p>With this account, you have unlocked access to:</p>
        <ul style='color: #94a3b8; font-size: 13px; line-height: 1.6;'>
            <li>All 98 Industry Topics across 4 progressive mastery stages</li>
            <li>FilteredElementCollector In-Memory Query Sandbox</li>
            <li>Autodesk Revit .addin Manifest Generator & Solution Scaffolder</li>
            <li>WPF MVVM Architecture for Advanced Modeless Dockable Panes</li>
            <li>Direct Technical Support & Official Certificate of Mastery</li>
        </ul>

        <div style='text-align: center; margin: 25px 0;'>
            <a href='{portalUrl}' class='btn'>Launch Workstation & Sign In &rarr;</a>
        </div>

        <div class='footer'>
            <p>Admin Contact: <a href='mailto:{AdminEmail}' style='color: #00f0ff;'>{AdminEmail}</a></p>
            <p>Please keep your login credentials secure. You can update your password at any time in your Account Profile tab.</p>
        </div>
    </div>
</body>
</html>";

        var msg = await SendEmailInternalAsync(recipientEmail, recipientName, subject, bodyHtml, EmailType.UserCredentials);

        // Also dispatch confirmation notice to the Admin email
        await SendAdminNotificationAsync(
            $"New User Enrolled: {recipientName} ({username})", 
            $"<p>The candidate <strong>{recipientName}</strong> was enrolled as <strong>{role}</strong> with username <code>{username}</code>. Credentials have been dispatched to <code>{recipientEmail}</code>.</p>");

        return msg;
    }

    public async Task<EmailMessage> SendAdminPasswordResetEmailAsync(string adminEmail, string resetToken, string resetUrl)
    {
        var subject = "Admin Password Reset Verification Token - Revit API Academy";
        var bodyHtml = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'/>
    <style>
        body {{ font-family: 'Segoe UI', Helvetica, Arial, sans-serif; background-color: #0b111e; color: #f8fafc; padding: 20px; margin: 0; }}
        .card {{ background-color: #10192a; border: 1px solid #a855f7; border-radius: 8px; max-width: 600px; margin: 0 auto; padding: 30px; box-shadow: 0 10px 30px rgba(0,0,0,0.5); }}
        .title {{ color: #c084fc; font-size: 20px; font-weight: bold; margin: 0; }}
        .token-box {{ background: #070b14; border: 1px solid #a855f7; text-align: center; padding: 20px; border-radius: 6px; margin: 20px 0; }}
        .token {{ color: #e9d5ff; font-size: 32px; font-weight: bold; letter-spacing: 0.15em; font-family: monospace; }}
        .footer {{ font-size: 12px; color: #64748b; margin-top: 25px; border-top: 1px solid rgba(255,255,255,0.08); padding-top: 15px; }}
    </style>
</head>
<body>
    <div class='card'>
        <div class='title'>SECURITY CLEARANCE: ADMIN PASSWORD RESET</div>
        <p>A request was received to reset the administrator password for the Autodesk Revit API & C# Master Academy.</p>
        <p>Use the single-use 6-digit verification token below to complete your password update:</p>

        <div class='token-box'>
            <div style='font-size: 12px; color: #94a3b8; margin-bottom: 8px;'>VERIFICATION RESET TOKEN</div>
            <div class='token'>{resetToken}</div>
        </div>

        <p>Return to the Admin Login portal and paste this token alongside your new password:</p>
        <p><a href='{resetUrl}' style='color: #00f0ff; font-family: monospace;'>{resetUrl}</a></p>

        <div class='footer'>
            <p>Recipient: <strong>{adminEmail}</strong></p>
            <p>If you did not initiate this reset, please review your account immediately.</p>
        </div>
    </div>
</body>
</html>";

        return await SendEmailInternalAsync(adminEmail, "Lead Administrator", subject, bodyHtml, EmailType.PasswordReset);
    }

    public async Task<EmailMessage> SendAdminNotificationAsync(string subject, string messageHtml)
    {
        var bodyHtml = $@"
<!DOCTYPE html>
<html>
<body style='font-family: Arial, sans-serif; background-color: #0b111e; color: #f8fafc; padding: 20px;'>
    <div style='background: #10192a; border: 1px solid #00f0ff; border-radius: 8px; max-width: 600px; margin: 0 auto; padding: 25px;'>
        <h3 style='color: #00f0ff; margin-top: 0;'>Academy Admin Notification</h3>
        {messageHtml}
        <div style='margin-top: 20px; font-size: 12px; color: #64748b; border-top: 1px solid rgba(255,255,255,0.1); padding-top: 10px;'>
            Sent to Primary Admin: {AdminEmail} &bull; Timestamp: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC
        </div>
    </div>
</body>
</html>";

        return await SendEmailInternalAsync(AdminEmail, "Academy Administrator", subject, bodyHtml, EmailType.SystemNotification);
    }

    private async Task<EmailMessage> SendEmailInternalAsync(
        string recipientEmail, 
        string recipientName, 
        string subject, 
        string bodyHtml, 
        EmailType type)
    {
        var emailRecord = new EmailMessage
        {
            RecipientEmail = recipientEmail,
            RecipientName = recipientName,
            Subject = subject,
            BodyHtml = bodyHtml,
            Type = type,
            SentAt = DateTime.UtcNow
        };

        // If SMTP password is configured, attempt real SMTP transmission
        if (!string.IsNullOrWhiteSpace(SmtpPassword))
        {
            try
            {
                using var client = new SmtpClient(SmtpHost, SmtpPort)
                {
                    EnableSsl = EnableSsl,
                    Credentials = new NetworkCredential(SmtpUsername, SmtpPassword),
                    Timeout = 10000
                };

                using var mailMsg = new MailMessage
                {
                    From = new MailAddress(SenderEmail, SenderName),
                    Subject = subject,
                    Body = bodyHtml,
                    IsBodyHtml = true
                };

                mailMsg.To.Add(new MailAddress(recipientEmail, recipientName));

                await client.SendMailAsync(mailMsg);
                emailRecord.IsSuccess = true;
                emailRecord.Status = "Delivered via Live SMTP";
            }
            catch (Exception ex)
            {
                emailRecord.IsSuccess = false;
                emailRecord.Status = "SMTP Error (Queued in Outbox)";
                emailRecord.ErrorMessage = ex.Message;
            }
        }
        else
        {
            // Smtp not yet configured with password - recorded in Outbox
            emailRecord.IsSuccess = false;
            emailRecord.Status = "Pending App Password (Outbox Saved)";
            emailRecord.ErrorMessage = "Google App Password is not yet configured. Recorded in Outbox for preview and manual resend.";
        }

        lock (_outbox)
        {
            _outbox.Add(emailRecord);
        }

        OnEmailDispatched?.Invoke();
        return emailRecord;
    }
}
