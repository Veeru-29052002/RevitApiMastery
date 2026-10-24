using RevitApiMastery.Models;

namespace RevitApiMastery.Services;

public class SupportService
{
    private readonly List<SupportTicket> _tickets = new();
    private readonly List<FaqItem> _faqs = new();

    public event Action? OnTicketsChanged;
    private readonly EmailService? _emailService;

    public SupportService(EmailService? emailService = null)
    {
        _emailService = emailService;
        InitializeSeedData();
    }

    private void InitializeSeedData()
    {
        _tickets.AddRange(new[]
        {
            new SupportTicket
            {
                Id = "TICK-101",
                Title = "InvalidOperationException: Starting a transaction outside document context",
                Description = "When I invoke `using (Transaction t = new Transaction(doc))`, Revit throws an InvalidOperationException saying 'Starting a transaction from an invalid context'. I'm calling this inside a background worker thread.",
                ErrorMessage = "Autodesk.Revit.Exceptions.InvalidOperationException: Starting a transaction from an external thread is not allowed.",
                CodeSnippet = "Task.Run(() => {\n    using (Transaction t = new Transaction(doc, \"Create Walls\")) {\n        t.Start();\n        // Throws here!\n        t.Commit();\n    }\n});",
                Category = TicketCategory.TransactionsAndDMU,
                Priority = TicketPriority.Urgent,
                Status = TicketStatus.InProgress,
                ModuleReference = "Module R2: Transactions & Document Lifecycle",
                AuthorId = "usr-student-01",
                AuthorName = "Alex Rivera, EIT",
                AuthorEmail = "student@revitapi.com",
                AuthorRole = UserRole.User,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddHours(-4),
                Messages = new List<TicketMessage>
                {
                    new TicketMessage
                    {
                        Id = "msg-1",
                        SenderName = "Alex Rivera, EIT",
                        SenderRole = UserRole.User,
                        MessageText = "Here is my code where the crash happens. How can I safely run heavy calculations without freezing Revit?",
                        CodeSnippet = "// Thread call crashing in Revit 2025\nTask.Run(() => doc.Create.NewFamilyInstance(...));",
                        SentAt = DateTime.UtcNow.AddDays(-2)
                    },
                    new TicketMessage
                    {
                        Id = "msg-2",
                        SenderName = "Sarah Chen, PE (Admin)",
                        SenderRole = UserRole.Admin,
                        MessageText = "Hi Alex! Revit's API is strictly single-threaded. You CANNOT execute transactions or touch the Revit DB inside Task.Run(). Instead, compute your data on the background thread, and then raise an `IExternalEventHandler` or use the main UI thread to commit the transaction.",
                        CodeSnippet = "// Pattern: Compute async -> Execute via IExternalEventHandler\npublic class WallCreatorHandler : IExternalEventHandler {\n    public void Execute(UIApplication app) {\n        using var t = new Transaction(app.ActiveUIDocument.Document, \"Async Walls\");\n        t.Start();\n        // Safe Revit API calls here on the main Revit thread!\n        t.Commit();\n    }\n    public string GetName() => \"WallCreator\";\n}",
                        SentAt = DateTime.UtcNow.AddHours(-4)
                    }
                }
            },
            new SupportTicket
            {
                Id = "TICK-102",
                Title = "WherePasses(ElementLevelFilter) returns 0 elements for Wall Types",
                Description = "I am attempting to get all Wall Types placed on Level 1, but WhereElementIsElementType paired with ElementLevelFilter returns zero items. Is this expected behavior?",
                CodeSnippet = "var col = new FilteredElementCollector(doc)\n    .OfCategory(BuiltInCategory.OST_Walls)\n    .WhereElementIsElementType()\n    .WherePasses(new ElementLevelFilter(levelId));",
                Category = TicketCategory.RevitApiExceptions,
                Priority = TicketPriority.Medium,
                Status = TicketStatus.Resolved,
                ModuleReference = "Module R3: FilteredElementCollector",
                AuthorId = "usr-student-02",
                AuthorName = "David Miller",
                AuthorEmail = "david.m@aecom.com",
                AuthorRole = UserRole.User,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-3),
                Messages = new List<TicketMessage>
                {
                    new TicketMessage
                    {
                        Id = "msg-3",
                        SenderName = "David Miller",
                        SenderRole = UserRole.User,
                        MessageText = "Filter count is always 0. Please help clarify why types don't have levels.",
                        SentAt = DateTime.UtcNow.AddDays(-5)
                    },
                    new TicketMessage
                    {
                        Id = "msg-4",
                        SenderName = "Sarah Chen, PE (Admin)",
                        SenderRole = UserRole.Admin,
                        MessageText = "ElementLevelFilter only applies to element instances! Wall Types (`ElementType` / `WallType`) belong to the project definition library and have no geometric elevation or Level assignment. Only placed Wall instances (`WhereElementIsNotElementType()`) have a LevelId.",
                        CodeSnippet = "// Correct Instance Collector\nvar instances = new FilteredElementCollector(doc)\n    .OfCategory(BuiltInCategory.OST_Walls)\n    .WhereElementIsNotElementType()\n    .WherePasses(new ElementLevelFilter(levelId));",
                        SentAt = DateTime.UtcNow.AddDays(-3)
                    }
                }
            },
            new SupportTicket
            {
                Id = "TICK-103",
                Title = "DockablePanel WPF control crashing on RegisterDockablePane in Revit 2025",
                Description = "We are migrating our add-in to .NET 8 for Revit 2025. During IExternalApplication.OnStartup, RegisterDockablePane throws a BadImageFormatException.",
                ErrorMessage = "System.BadImageFormatException: An attempt was made to load a program with an incorrect format.",
                Category = TicketCategory.WpfAndMvvm,
                Priority = TicketPriority.Urgent,
                Status = TicketStatus.Open,
                ModuleReference = "Module R9: UI, MVVM & Dockable Panels",
                AuthorId = "usr-student-03",
                AuthorName = "Elena Rostova",
                AuthorEmail = "elena.r@burohappold.com",
                AuthorRole = UserRole.User,
                CreatedAt = DateTime.UtcNow.AddHours(-8),
                UpdatedAt = DateTime.UtcNow.AddHours(-8),
                Messages = new List<TicketMessage>
                {
                    new TicketMessage
                    {
                        Id = "msg-5",
                        SenderName = "Elena Rostova",
                        SenderRole = UserRole.User,
                        MessageText = "The project compiled fine, but when launching Revit 2025 the dockable pane registration crashes immediately with BadImageFormatException.",
                        SentAt = DateTime.UtcNow.AddHours(-8)
                    }
                }
            },
            new SupportTicket
            {
                Id = "TICK-104",
                Title = "NullReferenceException when checking Parameter.AsString() on Double parameter",
                Description = "When looping through all parameter definitions, `param.AsString()` returns null even though `param.HasValue` is true for Area and Volume.",
                Category = TicketCategory.CSharpFundamentals,
                Priority = TicketPriority.Low,
                Status = TicketStatus.Resolved,
                ModuleReference = "Module R4: Parameters & Shared Parameters",
                AuthorId = "usr-student-04",
                AuthorName = "Liam Patel",
                AuthorEmail = "liam.p@arup.com",
                AuthorRole = UserRole.User,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
                UpdatedAt = DateTime.UtcNow.AddDays(-6),
                Messages = new List<TicketMessage>
                {
                    new TicketMessage
                    {
                        Id = "msg-6",
                        SenderName = "Liam Patel",
                        SenderRole = UserRole.User,
                        MessageText = "Why doesn't AsString() convert double numbers to strings automatically?",
                        SentAt = DateTime.UtcNow.AddDays(-7)
                    },
                    new TicketMessage
                    {
                        Id = "msg-7",
                        SenderName = "Sarah Chen, PE (Admin)",
                        SenderRole = UserRole.Admin,
                        MessageText = "In the Revit API, `param.AsString()` ONLY returns non-null for StorageType.String. For numerical parameters (Double/Integer), you must call `param.AsDouble()` or use `param.AsValueString()` if you want the project-formatted text representation (e.g. '15.4 m²').",
                        SentAt = DateTime.UtcNow.AddDays(-6)
                    }
                }
            }
        });

        // Initialize FAQs
        _faqs.AddRange(new[]
        {
            new FaqItem
            {
                Category = "Threading & Context",
                Question = "Why does Revit crash or throw InvalidOperationException when I use Task.Run or async/await?",
                AnswerHtml = "The Autodesk Revit API is <strong>strictly single-threaded</strong> and runs exclusively on the main Revit execution thread. Calling API methods from worker threads, background timers, or unbounded <code>Task.Run</code> calls will corrupt the internal C++ memory or trigger an immediate <code>InvalidOperationException</code>. To dispatch work from background tasks back to Revit, always use an <strong><code>IExternalEventHandler</code></strong> or register a modeless dispatch event.",
                CodeSample = "public class SafeAsyncHandler : IExternalEventHandler\n{\n    public void Execute(UIApplication app)\n    {\n        // Runs safely on the Revit main thread\n        using var t = new Transaction(app.ActiveUIDocument.Document, \"Async Action\");\n        t.Start();\n        // do work\n        t.Commit();\n    }\n    public string GetName() => \"SafeAsyncHandler\";\n}"
            },
            new FaqItem
            {
                Category = "Revit 2025 (.NET 8 Migration)",
                Question = "How do I upgrade my Revit add-in from .NET Framework 4.8 (Revit 2024) to .NET 8 (Revit 2025+)?",
                AnswerHtml = "In Revit 2025, Autodesk moved the CLR host from .NET Framework 4.8 to modern <strong>.NET 8.0-windows</strong>. You must update your <code>.csproj</code> target framework to <code>&lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;</code>, enable <code>&lt;UseWPF&gt;true&lt;/UseWPF&gt;</code>, and ensure referenced dependencies do not target incompatible 32-bit binaries or outdated assemblies.",
                CodeSample = "<Project Sdk=\"Microsoft.NET.Sdk\">\n  <PropertyGroup>\n    <TargetFramework>net8.0-windows</TargetFramework>\n    <UseWPF>true</UseWPF>\n    <PlatformTarget>x64</PlatformTarget>\n    <ImplicitUsings>enable</ImplicitUsings>\n  </PropertyGroup>\n</Project>"
            },
            new FaqItem
            {
                Category = "FilteredElementCollector",
                Question = "Why is WherePasses() faster than using LINQ .Where() with FilteredElementCollector?",
                AnswerHtml = "<code>FilteredElementCollector</code> passes quick filters down directly to native C++ Revit memory BEFORE serializing managed C# wrapper objects. If you evaluate filters using managed LINQ <code>.ToElements().Where(...)</code>, Revit has to instantiate hundreds of thousands of C# wrappers across the CLR boundary, incurring a <strong>10x to 50x performance penalty</strong>.",
                CodeSample = "// FAST (Native C++ Quick Filter):\ncollector.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType();\n\n// SLOW (Avoid in Production):\ncollector.ToElements().Where(e => e.Category.Name == \"Walls\");"
            },
            new FaqItem
            {
                Category = "Transactions & SubTransactions",
                Question = "What is the difference between Transaction, SubTransaction, and TransactionGroup?",
                AnswerHtml = "A <strong>Transaction</strong> is the atomic unit of change committed to the Revit undo stack. A <strong>SubTransaction</strong> allows partitioning modifications inside an active transaction to rollback partial steps. A <strong>TransactionGroup</strong> aggregates multiple separate Transactions into a single undo entry (via <code>Assimilate()</code>) so the user can undo the entire tool action in one Ctrl+Z.",
                CodeSample = "using (var tg = new TransactionGroup(doc, \"Generate Building Grid\"))\n{\n    tg.Start();\n    // Run multiple individual transactions...\n    tg.Assimilate(); // Compresses all transactions into 1 Undo step!\n}"
            },
            new FaqItem
            {
                Category = "Manifests (.addin)",
                Question = "Where do I install .addin files so Revit detects my plugin automatically?",
                AnswerHtml = "Revit looks for manifests in two locations: <strong>All Users</strong>: <code>%ProgramData%\\Autodesk\\Revit\\Addins\\[Year]\\</code> and <strong>Current User</strong>: <code>%AppData%\\Autodesk\\Revit\\Addins\\[Year]\\</code>. You can generate verified XML manifests instantly in our <a href=\"manifest-tool\" class=\"text-cyan\">Manifest Tool</a>.",
                CodeSample = "<!-- Path: %AppData%\\Autodesk\\Revit\\Addins\\2025\\MyAddin.addin -->\n<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<RevitAddIns>\n  <AddIn Type=\"Application\">\n    <Name>RevitMasterApp</Name>\n    <Assembly>C:\\Plugins\\MyAddin.dll</Assembly>\n    <AddInId>12345678-ABCD-EF01-2345-6789ABCDEF01</AddInId>\n    <FullClassName>MyAddin.App</FullClassName>\n    <VendorId>ACADEMY</VendorId>\n  </AddIn>\n</RevitAddIns>"
            },
            new FaqItem
            {
                Category = "Account & Passwords",
                Question = "What should I do if I forget my login password?",
                AnswerHtml = "<strong>For Administrators:</strong> Click <em>'Forgot password?'</em> on the Admin Login tab to receive a secure password reset link and verification token sent directly to your registered administrator email (<code>bveerababu612@gmail.com</code>).<br/><br/><strong>For Students / Users:</strong> To protect academic credentials and verified certificate records, student password resets are handled directly by instructors. Please submit a support ticket under the <code>Student Account & Password Recovery</code> category with your full name, and an administrator will securely update your credentials from the Admin Console.",
                CodeSample = "// Support Category: Student Account & Password Recovery\n// Contact: Helpdesk ticket or directly reach out to Academy Administrator"
            }
        });
    }

    public IReadOnlyList<SupportTicket> GetTickets() => _tickets.OrderByDescending(t => t.CreatedAt).ToList();

    public SupportTicket? GetTicketById(string id) =>
        _tickets.FirstOrDefault(t => string.Equals(t.Id, id, StringComparison.OrdinalIgnoreCase));

    public SupportTicket CreateTicket(SupportTicket ticket)
    {
        if (string.IsNullOrWhiteSpace(ticket.Id))
            ticket.Id = "TICK-" + new Random().Next(105, 999);

        ticket.CreatedAt = DateTime.UtcNow;
        ticket.Status = TicketStatus.Open;
        _tickets.Insert(0, ticket);

        // Notify Admin (bveerababu612@gmail.com) of the incoming ticket
        if (_emailService != null)
        {
            _ = _emailService.SendAdminNotificationAsync(
                $"New Support Ticket #{ticket.Id}: {ticket.Title}",
                $"<p><strong>Student:</strong> {ticket.AuthorName} ({ticket.AuthorEmail})</p><p><strong>Subject:</strong> {ticket.Title}</p><p><strong>Category:</strong> {ticket.Category} &bull; <strong>Priority:</strong> {ticket.Priority}</p><p>{ticket.Description}</p>");
        }

        OnTicketsChanged?.Invoke();
        return ticket;
    }

    public void AddReply(string ticketId, string messageText, string senderName, UserRole senderRole, string? codeSnippet = null)
    {
        var ticket = GetTicketById(ticketId);
        if (ticket == null || string.IsNullOrWhiteSpace(messageText)) return;

        ticket.Messages.Add(new TicketMessage
        {
            Id = Guid.NewGuid().ToString(),
            SenderName = senderName,
            SenderRole = senderRole,
            MessageText = messageText,
            CodeSnippet = codeSnippet,
            SentAt = DateTime.UtcNow
        });

        ticket.UpdatedAt = DateTime.UtcNow;

        // If an admin replies and status is open, automatically promote to InProgress
        if (senderRole == UserRole.Admin && ticket.Status == TicketStatus.Open)
        {
            ticket.Status = TicketStatus.InProgress;
        }

        OnTicketsChanged?.Invoke();
    }

    public void UpdateStatus(string ticketId, TicketStatus newStatus)
    {
        var ticket = GetTicketById(ticketId);
        if (ticket != null)
        {
            ticket.Status = newStatus;
            ticket.UpdatedAt = DateTime.UtcNow;
            OnTicketsChanged?.Invoke();
        }
    }

    public IReadOnlyList<FaqItem> GetFaqs() => _faqs;

    public (int Total, int Open, int InProgress, int Resolved) GetTicketStats()
    {
        var total = _tickets.Count;
        var open = _tickets.Count(t => t.Status == TicketStatus.Open);
        var inProg = _tickets.Count(t => t.Status == TicketStatus.InProgress);
        var res = _tickets.Count(t => t.Status == TicketStatus.Resolved);
        return (total, open, inProg, res);
    }
}
