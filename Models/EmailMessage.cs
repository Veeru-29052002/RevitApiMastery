namespace RevitApiMastery.Models;

public enum EmailType
{
    UserCredentials,
    PasswordReset,
    SystemNotification,
    SupportTicketNotice
}

public class EmailMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string RecipientEmail { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string BodyHtml { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool IsSuccess { get; set; }
    public string Status { get; set; } = "Sent";
    public string? ErrorMessage { get; set; }
    public EmailType Type { get; set; }
}
