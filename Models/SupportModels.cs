namespace RevitApiMastery.Models;

public enum TicketCategory
{
    CSharpFundamentals,
    RevitApiExceptions,
    TransactionsAndDMU,
    WpfAndMvvm,
    ManifestAndDeployment,
    GeneralAcademy,
    AccountAndCredentials
}

public enum TicketPriority
{
    Low,
    Medium,
    High,
    Urgent
}

public enum TicketStatus
{
    Open,
    InProgress,
    Resolved,
    Closed
}

public class SupportTicket
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CodeSnippet { get; set; }
    public string? ErrorMessage { get; set; }
    public TicketCategory Category { get; set; } = TicketCategory.RevitApiExceptions;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public string? ModuleReference { get; set; } // e.g. "Module R3: Collectors"
    public string AuthorId { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorEmail { get; set; } = string.Empty;
    public UserRole AuthorRole { get; set; } = UserRole.User;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public List<TicketMessage> Messages { get; set; } = new();
}

public class TicketMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SenderName { get; set; } = string.Empty;
    public UserRole SenderRole { get; set; } = UserRole.User;
    public string MessageText { get; set; } = string.Empty;
    public string? CodeSnippet { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}

public class FaqItem
{
    public string Question { get; set; } = string.Empty;
    public string AnswerHtml { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? CodeSample { get; set; }
}
