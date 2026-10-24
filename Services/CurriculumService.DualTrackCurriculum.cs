using RevitApiMastery.Models;

namespace RevitApiMastery.Services;

public partial class CurriculumService
{
    private void BuildDualTrackCurriculum(
        Module modCsLvl1,
        Module modCsAdv,
        Module modRevitLvl1,
        Module modRevitMaster,
        out List<Module> csharpModules,
        out List<Module> revitModules)
    {
        csharpModules = new List<Module>();
        revitModules = new List<Module>();

        // Dictionary of all topics by ID for fast lookup
        var allRawTopics = new Dictionary<string, Topic>(StringComparer.OrdinalIgnoreCase);
        foreach (var t in modCsLvl1.Topics.Concat(modCsAdv.Topics).Concat(modRevitLvl1.Topics).Concat(modRevitMaster.Topics))
        {
            if (!allRawTopics.ContainsKey(t.Id))
                allRawTopics[t.Id] = t;
        }


        // ==========================================
        // TRACK 1: C# PROGRAMMING (MODULES C1 TO C10)
        // ==========================================

        // Module C1: C# Syntax, Types & Control Flow
        var modC1 = new Module
        {
            Id = "c-mod-1",
            ModuleCode = "C1",
            TrackId = "csharp",
            Order = 1,
            LevelNumber = 1,
            LevelTitle = "Stage 1: Syntax & Control Flow",
            Title = "Module C1: C# Syntax, Types & Control Flow",
            ShortDescription = "Foundational variables, primitive types, operators, conditionals, and looping structures.",
            Icon = "bi-terminal",
            Level = DifficultyLevel.Beginner
        };
        for (int i = 1; i <= 13; i++)
        {
            var tid = $"c-l1-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modC1.Id;
                modC1.Topics.Add(t);
            }
        }
        csharpModules.Add(modC1);

        // Module C2: Methods, Memory & Value vs Ref Types
        var modC2 = new Module
        {
            Id = "c-mod-2",
            ModuleCode = "C2",
            TrackId = "csharp",
            Order = 2,
            LevelNumber = 1,
            LevelTitle = "Stage 1: Methods & Memory",
            Title = "Module C2: Methods, Memory & Value vs Ref Types",
            ShortDescription = "Methods, parameters (ref, out, in), Stack vs Heap memory, structs, and nullability.",
            Icon = "bi-cpu",
            Level = DifficultyLevel.Beginner
        };
        for (int i = 14; i <= 18; i++)
        {
            var tid = $"c-l1-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modC2.Id;
                modC2.Topics.Add(t);
            }
        }
        csharpModules.Add(modC2);

        // Module C3: OOP Mastery - Classes, Interfaces & Polymorphism
        var modC3 = new Module
        {
            Id = "c-mod-3",
            ModuleCode = "C3",
            TrackId = "csharp",
            Order = 3,
            LevelNumber = 2,
            LevelTitle = "Stage 2: OOP Mastery",
            Title = "Module C3: OOP Mastery - Classes, Interfaces & Polymorphism",
            ShortDescription = "Classes, encapsulation, inheritance, polymorphism, abstract classes, and interfaces.",
            Icon = "bi-diagram-3",
            Level = DifficultyLevel.Intermediate
        };
        for (int i = 1; i <= 6; i++)
        {
            var tid = $"c-adv-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modC3.Id;
                modC3.Topics.Add(t);
            }
        }
        csharpModules.Add(modC3);

        // Module C4: Collections, Generics & Data Structures
        var modC4 = new Module
        {
            Id = "c-mod-4",
            ModuleCode = "C4",
            TrackId = "csharp",
            Order = 4,
            LevelNumber = 2,
            LevelTitle = "Stage 2: Collections & Generics",
            Title = "Module C4: Collections, Generics & Data Structures",
            ShortDescription = "Arrays, List<T>, Dictionary, HashSet, Queue, Stack, and Generic Type Constraints.",
            Icon = "bi-collection",
            Level = DifficultyLevel.Intermediate
        };
        for (int i = 19; i <= 22; i++)
        {
            var tid = $"c-l1-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modC4.Id;
                modC4.Topics.Add(t);
            }
        }
        if (allRawTopics.TryGetValue("c-adv-07", out var tGen))
        {
            tGen.ModuleId = modC4.Id;
            modC4.Topics.Add(tGen);
        }
        csharpModules.Add(modC4);

        // Module C5: LINQ & Functional C#
        var modC5 = new Module
        {
            Id = "c-mod-5",
            ModuleCode = "C5",
            TrackId = "csharp",
            Order = 5,
            LevelNumber = 2,
            LevelTitle = "Stage 2: LINQ & Functional C#",
            Title = "Module C5: LINQ & Functional C#",
            ShortDescription = "Query & Method syntax, Select, Where, OrderBy, GroupBy, Any, All, and deferred execution.",
            Icon = "bi-funnel",
            Level = DifficultyLevel.Intermediate
        };
        for (int i = 23; i <= 28; i++)
        {
            var tid = $"c-l1-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modC5.Id;
                modC5.Topics.Add(t);
            }
        }
        csharpModules.Add(modC5);

        // Module C6: Delegates, Events & Lambda Expressions
        var modC6 = new Module
        {
            Id = "c-mod-6",
            ModuleCode = "C6",
            TrackId = "csharp",
            Order = 6,
            LevelNumber = 3,
            LevelTitle = "Stage 3: Delegates & Events",
            Title = "Module C6: Delegates, Events & Lambda Expressions",
            ShortDescription = "Action<T>, Func<T, TResult>, Predicate, lambdas, and event publisher/subscriber architectures.",
            Icon = "bi-lightning-charge",
            Level = DifficultyLevel.Advanced
        };
        for (int i = 8; i <= 10; i++)
        {
            var tid = $"c-adv-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modC6.Id;
                modC6.Topics.Add(t);
            }
        }
        csharpModules.Add(modC6);

        // Module C7: Exception Handling & Defensive Coding
        var modC7 = new Module
        {
            Id = "c-mod-7",
            ModuleCode = "C7",
            TrackId = "csharp",
            Order = 7,
            LevelNumber = 3,
            LevelTitle = "Stage 3: Exception Handling",
            Title = "Module C7: Exception Handling & Defensive Coding",
            ShortDescription = "try/catch/finally, exception filtering, custom exceptions, and IDisposable resource cleanup.",
            Icon = "bi-shield-exclamation",
            Level = DifficultyLevel.Advanced
        };
        for (int i = 11; i <= 12; i++)
        {
            var tid = $"c-adv-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modC7.Id;
                modC7.Topics.Add(t);
            }
        }
        csharpModules.Add(modC7);

        // Module C8: Asynchronous Programming - async/await & Tasks
        var modC8 = new Module
        {
            Id = "c-mod-8",
            ModuleCode = "C8",
            TrackId = "csharp",
            Order = 8,
            LevelNumber = 3,
            LevelTitle = "Stage 3: Async Programming",
            Title = "Module C8: Asynchronous Programming - async/await & Tasks",
            ShortDescription = "Task, Task<T>, ConfigureAwait, deadlock avoidance, and Revit's single-threaded UI constraints.",
            Icon = "bi-hourglass-split",
            Level = DifficultyLevel.Advanced
        };
        if (allRawTopics.TryGetValue("c-adv-13", out var tAsync))
        {
            tAsync.ModuleId = modC8.Id;
            modC8.Topics.Add(tAsync);
        }
        modC8.Topics.Add(new Topic
        {
            Id = "c-c8-02",
            ModuleId = modC8.Id,
            TrackId = "csharp",
            Order = 2,
            Title = "Revit Single-Threaded Apartment (STA) & Async Dispatching",
            Subtitle = "Why background tasks crash when touching Revit Document and how to marshal calls",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Advanced,
            KeyTakeaways = new()
            {
                "Autodesk Revit requires all DB modifications and read operations to happen strictly on the main Revit UI thread.",
                "Spawning Task.Run(() => doc.Create.NewFamilyInstance(...)) throws an immediate InvalidOperationException / AccessViolation.",
                "To communicate between asynchronous background threads and Revit, you must use IExternalEventHandler / ExternalEvent."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The STA Thread Rule of Revit",
                    ContentHtml = "<p>Revit's database is not thread-safe. When your background worker finishes downloading BIM data or parsing an Excel sheet, you <strong>cannot</strong> directly invoke Revit transactions on that background thread. You must dispatch execution back to Revit using an <code>ExternalEvent</code>.</p>",
                    CalloutText = "Never call doc.Regenerate() or Transaction.Commit() from a Task.Run() worker thread.",
                    CalloutType = "warning"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Safe Background Worker with Revit Event Dispatcher",
                    Description = "Pattern for fetching web API data asynchronously without blocking Revit UI.",
                    Code = @"// Fetch cloud data asynchronously, then notify Revit handler
public async Task DownloadAndSyncBimDataAsync(ExternalEvent revitEvent, SyncEventHandler handler)
{
    using var httpClient = new HttpClient();
    var json = await httpClient.GetStringAsync(""https://api.bimplatform.com/v1/schedule"");
    
    // Store result safely in handler payload
    handler.CachedPayload = json;
    
    // Wake up Revit to execute on the main thread safely
    revitEvent.Raise();
}",
                    Explanation = "The network I/O is non-blocking, while the Revit write operation executes safely on the main thread."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Accessing Revit Document inside Task.Run",
                    Description = "Fatal crash or silent DB corruption caused by multi-threaded Revit API calls.",
                    BadCode = @"Task.Run(() => {
    // FATAL: Revit API is not thread-safe!
    using var tx = new Transaction(doc, ""Crash"");
    tx.Start();
    doc.Delete(elementId);
    tx.Commit();
});",
                    GoodCode = @"// Perform CPU or network work on task:
var result = await Task.Run(() => ComputeHeavyMath(pointCloudData));

// Then execute transaction on main Revit thread via ExternalEvent or direct modal command:
using var tx = new Transaction(doc, ""Safe Update"");
tx.Start();
ApplyComputationToModel(doc, result);
tx.Commit();",
                    WhyItMatters = "Revit immediately aborts the application process if DB access is detected from non-UI threads."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What happens if you open a Transaction inside Task.Run(() => ...) in a Revit add-in?",
                    Options = new() { "It runs 2x faster in parallel", "It queues automatically", "It throws an InvalidOperationException / crashes Revit", "Revit ignores the transaction" },
                    CorrectOptionIndex = 2,
                    Explanation = "Revit enforces single-threaded execution on the main Revit UI thread; multi-threaded calls throw an exception or crash."
                }
            }
        });
        csharpModules.Add(modC8);

        // Module C9: Modern C# Features - Records, Pattern Matching
        var modC9 = new Module
        {
            Id = "c-mod-9",
            ModuleCode = "C9",
            TrackId = "csharp",
            Order = 9,
            LevelNumber = 3,
            LevelTitle = "Stage 3: Modern C# Features",
            Title = "Module C9: Modern C# Features - Records, Pattern Matching",
            ShortDescription = "Records, with-expressions, pattern matching, switch expressions, and primary constructors.",
            Icon = "bi-stars",
            Level = DifficultyLevel.Advanced
        };
        modC9.Topics.Add(new Topic
        {
            Id = "c-c9-01",
            ModuleId = modC9.Id,
            TrackId = "csharp",
            Order = 1,
            Title = "C# Records & Immutable BIM Data Transfer Objects (DTOs)",
            Subtitle = "Eliminating boilerplate with record class, record struct, value equality, and with-expressions",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Advanced,
            KeyTakeaways = new()
            {
                "Records provide concise syntax for immutable reference types with built-in value-based equality.",
                "Non-destructive mutation is achieved via the with keyword: var updated = original with { Height = 3200 };",
                "Ideal for Revit export/import workflows, caching element parameters, and passing DTOs between UI and Revit commands."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Why Records Excel for BIM Data Processing",
                    ContentHtml = "<p>In BIM automation, we constantly extract element parameters and transfer them to external APIs, Excel reports, or WPF views. Writing traditional classes with constructors, property getters, <code>Equals</code>, and <code>GetHashCode</code> overrides requires 50+ lines of boilerplate. A C# record does all of that in <strong>1 line</strong>.</p>",
                    CalloutText = "Records compare by value rather than reference address.",
                    CalloutType = "tip"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "BIM Element DTO using Positional Record",
                    Description = "Declaring an immutable BIM element data carrier.",
                    Code = @"// Positional record declaration in 1 line:
public record WallDto(int ElementId, string FamilyName, double LengthFeet, double HeightFeet);

// Usage:
var wall1 = new WallDto(10245, ""Generic 200mm"", 24.5, 10.0);

// Non-destructive mutation using 'with' expression:
var wall2 = wall1 with { HeightFeet = 12.0 };

// Built-in value equality:
bool areSame = (wall1 == new WallDto(10245, ""Generic 200mm"", 24.5, 10.0)); // True!",
                    Explanation = "Records automatically synthesize equality operators, string formatting, and cloning routines."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Mutating Records Accidentally",
                    Description = "Trying to change property values of positional records with init-only setters.",
                    BadCode = @"var wall = new WallDto(101, ""Partition"", 15.0, 9.0);
wall.HeightFeet = 10.5; // COMPILE ERROR: Init-only property cannot be assigned!",
                    GoodCode = @"var wall = new WallDto(101, ""Partition"", 15.0, 9.0);
// Use 'with' expression to produce a modified copy:
var updatedWall = wall with { HeightFeet = 10.5 };",
                    WhyItMatters = "Records guarantee thread-safe immutability across async processing pipelines."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Which keyword is used to create a modified clone of a record in C#?",
                    Options = new() { "clone", "copy", "with", "modify" },
                    CorrectOptionIndex = 2,
                    Explanation = "The 'with' expression performs non-destructive mutation on record types."
                }
            }
        });
        modC9.Topics.Add(new Topic
        {
            Id = "c-c9-02",
            ModuleId = modC9.Id,
            TrackId = "csharp",
            Order = 2,
            Title = "Pattern Matching & Switch Expressions for Revit Elements",
            Subtitle = "Eliminating complex if/else chains with type patterns, property patterns, and relational patterns",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Advanced,
            KeyTakeaways = new()
            {
                "Type Pattern Matching allows instantaneous casting without fragile is / as statements.",
                "Property Patterns test nested properties cleanly: element is Wall { StructuralUsage: StructuralUsage.Bearing }.",
                "Switch Expressions transform cumbersome switch statements into elegant functional value mappings."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Clean Revit Element Classification",
                    ContentHtml = "<p>In the Revit API, many specific classes inherit from <code>Element</code> (e.g. <code>Wall</code>, <code>FamilyInstance</code>, <code>Floor</code>, <code>ViewPlan</code>). Modern pattern matching allows you to handle each subclass with zero casting friction.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Switch Expression with Type & Property Patterns",
                    Description = "Extracting dimensional information based on element type.",
                    Code = @"public string DescribeElement(Element element) => element switch
{
    Wall { WallType.Kind: WallKind.Curtain } => ""Curtain Wall System"",
    Wall w => $""Standard Wall: Length {w.Width * 304.8:F0}mm"",
    FamilyInstance { StructuralType: StructuralType.Column } => ""Structural Column"",
    FamilyInstance fi => $""Component Instance: {fi.Symbol.FamilyName}"",
    Floor f => $""Floor Slab (ID: {f.Id})"",
    _ => $""Generic Element: {element.Category?.Name ?? ""Unknown""}""
};",
                    Explanation = "Compact, type-safe, and self-documenting logic."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Forgetting Discard Case in Switch Expression",
                    Description = "Omitting default discard '_' causes SwitchExpressionException at runtime when an unmatched element arrives.",
                    BadCode = @"// Missing discard (_) case:
string name = element switch {
    Wall w => ""Wall"",
    Floor f => ""Floor""
}; // Throws SwitchExpressionException if element is a Door!",
                    GoodCode = @"string name = element switch {
    Wall w => ""Wall"",
    Floor f => ""Floor"",
    _ => ""Other Element"" // Safe fallback discard!
};",
                    WhyItMatters = "Revit models contain hundreds of unexpected element types (Cameras, Sketched Lines, Extents)."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What symbol represents the default fallback in a C# switch expression?",
                    Options = new() { "default:", "*", "_", "else" },
                    CorrectOptionIndex = 2,
                    Explanation = "The underscore '_' is the discard pattern representing any unmatched input."
                }
            }
        });
        csharpModules.Add(modC9);

        // Module C10: Architecture & Design Patterns for Add-ins
        var modC10 = new Module
        {
            Id = "c-mod-10",
            ModuleCode = "C10",
            TrackId = "csharp",
            Order = 10,
            LevelNumber = 4,
            LevelTitle = "Stage 4: Software Architecture",
            Title = "Module C10: Architecture & Design Patterns for Add-ins",
            ShortDescription = "Dependency Injection, Factory Pattern, Repository Pattern, and MVVM Architecture for BIM plugins.",
            Icon = "bi-building-gear",
            Level = DifficultyLevel.Expert
        };
        modC10.Topics.Add(new Topic
        {
            Id = "c-c10-01",
            ModuleId = modC10.Id,
            TrackId = "csharp",
            Order = 1,
            Title = "Dependency Injection (DI) in Autodesk Revit Add-ins",
            Subtitle = "Structuring scalable add-in solutions with Microsoft.Extensions.DependencyInjection",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Expert,
            KeyTakeaways = new()
            {
                "Dependency Injection decouples command handlers from UI dialogs, database queries, and logging systems.",
                "In IExternalApplication.OnStartup, configure your ServiceCollection with Singletons and Transients.",
                "Service provider can be passed or accessed via a static ServiceLocator inside IExternalCommand.Execute."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Why Commercial Plugins Require DI",
                    ContentHtml = "<p>Monolithic Revit plugins tightly couple business logic inside <code>Execute()</code>. When requirements change, refactoring breaks everything. With <strong>Dependency Injection</strong>, services like <code>IBimAuditService</code>, <code>IExportService</code>, and <code>ILogger</code> are injected cleanly, enabling unit testing with mock Revit documents.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Configuring IServiceProvider in Revit Add-in App.cs",
                    Description = "Full DI bootstrapping pattern in OnStartup.",
                    Code = @"public class App : IExternalApplication
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public Result OnStartup(UIControlledApplication app)
    {
        var services = new ServiceCollection();
        
        // Register core application services
        services.AddSingleton<ILoggerService, FileLoggerService>();
        services.AddTransient<IWallAuditEngine, WallAuditEngine>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();

        ServiceProvider = services.BuildServiceProvider();
        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication app) => Result.Succeeded;
}",
                    Explanation = "Commands can resolve ViewModels and Services directly via App.ServiceProvider."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Registering Revit Document as a Singleton",
                    Description = "Revit Document changes when user switches active projects or opens multiple files.",
                    BadCode = @"// DISASTROUS: doc is transient per command execution!
services.AddSingleton<Document>(doc);",
                    GoodCode = @"// Pass Document into method execution or use scoped factory:
public interface IWallAuditEngine
{
    void ExecuteAudit(Document doc);
}",
                    WhyItMatters = "Users frequently have multiple Revit projects open; caching Document as Singleton causes memory leaks and cross-project corruption."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Why should the Revit Document NOT be registered as a Singleton in DI?",
                    Options = new() { "Revit does not support DI", "Document changes when users switch tabs or close models", "Document is a struct", "Singletons cannot hold reference types" },
                    CorrectOptionIndex = 1,
                    Explanation = "Users switch between multiple open Revit documents; a singleton would hold stale references to closed models."
                }
            }
        });
        modC10.Topics.Add(new Topic
        {
            Id = "c-c10-02",
            ModuleId = modC10.Id,
            TrackId = "csharp",
            Order = 2,
            Title = "Repository & Factory Patterns for BIM Elements",
            Subtitle = "Abstracting FilteredElementCollectors into reusable, testable repository classes",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Expert,
            KeyTakeaways = new()
            {
                "The Repository Pattern hides FilteredElementCollector plumbing behind intuitive methods (e.g. GetAllDoors()).",
                "The Factory Pattern encapsulates complex element generation (geometry validation, level binding, family placement).",
                "Centralizes all Revit DB querying to prevent code duplication across ribbon pushbuttons."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "BIM Element Repository Architecture",
                    ContentHtml = "<p>Instead of scattering <code>new FilteredElementCollector(doc).OfCategory(...)</code> across 20 different command files, encapsulate them in an <code>ElementRepository</code>. If Revit API changes in a future release, you update the query in <strong>one single place</strong>.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Clean Element Repository Implementation",
                    Description = "Standardized BIM query repository.",
                    Code = @"public class ElementRepository
{
    private readonly Document _doc;
    public ElementRepository(Document doc) => _doc = doc;

    public List<Wall> GetExteriorWalls() =>
        new FilteredElementCollector(_doc)
            .OfClass(typeof(Wall))
            .WhereElementIsNotElementType()
            .Cast<Wall>()
            .Where(w => w.WallType.Function == WallFunction.Exterior)
            .ToList();

    public List<FamilyInstance> GetDoorsOnLevel(ElementId levelId) =>
        new FilteredElementCollector(_doc)
            .OfCategory(BuiltInCategory.OST_Doors)
            .WhereElementIsNotElementType()
            .Cast<FamilyInstance>()
            .Where(d => d.LevelId == levelId)
            .ToList();
}",
                    Explanation = "Clean, reusable, and readable BIM element access."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Keeping Repository after Transaction Commits without Refresh",
                    Description = "Revit element references can become invalid if elements are deleted or undo is invoked.",
                    BadCode = @"var repo = new ElementRepository(doc);
var walls = repo.GetExteriorWalls();
// ... heavy modifications / undo ...
var len = walls[0].Width; // Potential ElementNullReference or stale cache!",
                    GoodCode = @"// Fetch elements within transaction scope or refresh IDs:
var wallIds = repo.GetExteriorWalls().Select(w => w.Id).ToList();
// In new transaction:
var wall = doc.GetElement(wallIds[0]) as Wall;",
                    WhyItMatters = "ElementIds remain valid across transaction boundaries, but in-memory element instances can be invalidated."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What is the primary benefit of the Repository Pattern in Revit development?",
                    Options = new() { "It speeds up WPF rendering", "It centralizes and abstracts collector queries into reusable methods", "It replaces the need for transactions", "It converts C# to Python" },
                    CorrectOptionIndex = 1,
                    Explanation = "Repositories abstract raw collectors into maintainable, reusable domain query methods."
                }
            }
        });
        csharpModules.Add(modC10);

        // ==========================================
        // TRACK 2: REVIT API C# (MODULES R1 TO R10)
        // ==========================================

        // Module R1: Foundations & Architecture
        var modR1 = new Module
        {
            Id = "r-mod-1",
            ModuleCode = "R1",
            TrackId = "revitapi",
            Order = 1,
            LevelNumber = 1,
            LevelTitle = "Stage 1: Foundations & Architecture",
            Title = "Module R1: Foundations & Architecture",
            ShortDescription = "Revit Internal DB, CLR Bridge, .NET 4.8 vs .NET 8, SDK csproj, debugging, and RevitAPIDocs.",
            Icon = "bi-layers-half",
            Level = DifficultyLevel.Beginner
        };
        for (int i = 1; i <= 3; i++)
        {
            var tid = $"r-l1-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modR1.Id;
                modR1.Topics.Add(t);
            }
        }
        revitModules.Add(modR1);

        // Module R2: Core Entry Points & UI
        var modR2 = new Module
        {
            Id = "r-mod-2",
            ModuleCode = "R2",
            TrackId = "revitapi",
            Order = 2,
            LevelNumber = 1,
            LevelTitle = "Stage 1: Entry Points & Ribbon UI",
            Title = "Module R2: Core Entry Points & UI",
            ShortDescription = "IExternalCommand, IExternalApplication, Ribbon Panels, PushButtons, Icons, and Selection API.",
            Icon = "bi-window-stack",
            Level = DifficultyLevel.Beginner
        };
        foreach (var idx in new[] { 4, 5, 6, 11 })
        {
            var tid = $"r-l1-{idx:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modR2.Id;
                modR2.Topics.Add(t);
            }
        }
        revitModules.Add(modR2);

        // Module R3: The Transaction Engine
        var modR3 = new Module
        {
            Id = "r-mod-3",
            ModuleCode = "R3",
            TrackId = "revitapi",
            Order = 3,
            LevelNumber = 2,
            LevelTitle = "Stage 2: The Transaction Engine",
            Title = "Module R3: The Transaction Engine",
            ShortDescription = "Transaction lifecycle (Start, Commit, RollBack), TransactionGroup, SubTransaction, and IFailuresPreprocessor.",
            Icon = "bi-arrow-repeat",
            Level = DifficultyLevel.Intermediate
        };
        foreach (var idx in new[] { 9, 10 })
        {
            var tid = $"r-l1-{idx:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modR3.Id;
                modR3.Topics.Add(t);
            }
        }
        modR3.Topics.Add(new Topic
        {
            Id = "r-r3-03",
            ModuleId = modR3.Id,
            TrackId = "revitapi",
            Order = 3,
            Title = "Error Suppression & IFailuresPreprocessor",
            Subtitle = "Silently swallowing harmless Revit warnings to prevent interrupting automated batch routines",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Revit displays interactive warning popups during transactions that halt automated headless processes.",
                "Implementing IFailuresPreprocessor allows your add-in to delete or resolve warnings programmatically.",
                "Must be registered with FailureHandlingOptions via transaction.SetFailureHandlingOptions(options)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The Automated Warning Resolution Mechanism",
                    ContentHtml = "<p>When placing hundreds of rooms or modifying room separation lines, Revit throws warning dialogues (e.g. 'Room is not in a properly enclosed region'). In headless batch processing, this halts execution. <code>IFailuresPreprocessor</code> intercepts these before any UI is rendered.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Warning Swallower Implementation",
                    Description = "Robust warning preprocessor in C#.",
                    Code = @"public class SilentWarningSwallower : IFailuresPreprocessor
{
    public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
    {
        // Delete all harmless warnings without prompting the user
        failuresAccessor.DeleteAllWarnings();
        return FailureProcessingResult.Continue;
    }
}

// In your command:
using (var tx = new Transaction(doc, ""Batch Processing""))
{
    var options = tx.GetFailureHandlingOptions();
    options.SetFailuresPreprocessor(new SilentWarningSwallower());
    tx.SetFailureHandlingOptions(options);

    tx.Start();
    // Batch operations...
    tx.Commit();
}",
                    Explanation = "Ensures automated scripts run without hanging on Revit warning popups."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Trying to Swallow Severe Errors",
                    Description = "IFailuresPreprocessor can delete warnings, but fatal errors require actual document resolution.",
                    BadCode = @"// Deleting errors that require element rollback:
failuresAccessor.DeleteAllWarnings(); // Only works for Severity.Warning!",
                    GoodCode = @"// Inspect severity and rollback if fatal:
foreach (var failure in failuresAccessor.GetFailureMessages()) {
    if (failure.GetSeverity() == FailureSeverity.Error) {
        failuresAccessor.ResolveFailure(failure);
    }
}",
                    WhyItMatters = "Ignoring genuine DB corruption errors can crash the entire Revit application session."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Which interface allows an add-in to suppress warning dialogues during transactions?",
                    Options = new() { "IWarningHandler", "IFailuresPreprocessor", "ITransactionSuppressor", "IAlertManager" },
                    CorrectOptionIndex = 1,
                    Explanation = "IFailuresPreprocessor handles failure messages during transaction commits."
                }
            }
        });
        revitModules.Add(modR3);

        // Module R4: Elements & FilteredElementCollector Mastery
        var modR4 = new Module
        {
            Id = "r-mod-4",
            ModuleCode = "R4",
            TrackId = "revitapi",
            Order = 4,
            LevelNumber = 2,
            LevelTitle = "Stage 2: Elements & Collectors",
            Title = "Module R4: Elements & FilteredElementCollector Mastery",
            ShortDescription = "ElementId, UniqueId, Quick Filters vs Slow Filters, OfCategory, OfClass, and Spatial Intersect Filters.",
            Icon = "bi-search",
            Level = DifficultyLevel.Intermediate
        };
        foreach (var idx in new[] { 7, 8 })
        {
            var tid = $"r-l1-{idx:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modR4.Id;
                modR4.Topics.Add(t);
            }
        }
        modR4.Topics.Add(new Topic
        {
            Id = "r-r4-03",
            ModuleId = modR4.Id,
            TrackId = "revitapi",
            Order = 3,
            Title = "Spatial Filters: BoundingBox & ElementIntersectsFilter",
            Subtitle = "Querying elements located inside 3D volumes without iterating the entire Revit database",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "BoundingBoxIntersectsFilter queries the Revit spatial octree index in native C++ with lightning speed.",
                "ElementIntersectsElementFilter performs accurate Boolean solid-intersection testing.",
                "Always apply QuickFilters (Category/Class) before spatial filters for optimal query performance."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The Power of Spatial Indexing",
                    ContentHtml = "<p>In a 500MB hospital model containing 250,000 elements, looping through all elements to check coordinates takes seconds. <strong>BoundingBoxIntersectsFilter</strong> queries Revit's native C++ spatial index, returning results in under 5 milliseconds.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "BoundingBox Spatial Collector Query",
                    Description = "Find all MEP ducts and pipes intersecting a room's bounding box.",
                    Code = @"// Get room bounding box
BoundingBoxXYZ roomBox = room.get_BoundingBox(null);
Outline outline = new Outline(roomBox.Min, roomBox.Max);

// Create native fast spatial filter
BoundingBoxIntersectsFilter spatialFilter = new BoundingBoxIntersectsFilter(outline);

// Collect intersecting pipes on native C++ layer
var pipesInRoom = new FilteredElementCollector(doc)
    .OfCategory(BuiltInCategory.OST_PipeCurves)
    .WherePasses(spatialFilter)
    .WhereElementIsNotElementType()
    .ToElements();",
                    Explanation = "Filters elements inside the bounding box before marshalling managed C# wrappers."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Iterating All Document Elements with C# LINQ for Distance Checks",
                    Description = "Massive performance degradation on large commercial projects.",
                    BadCode = @"// HORRIBLE: Pulls 200,000 managed objects into C# heap!
var nearby = new FilteredElementCollector(doc)
    .Where(e => e.get_BoundingBox(null)?.Min.DistanceTo(target) < 10);",
                    GoodCode = @"// FAST: Native C++ spatial filter eliminates 99.9% of elements:
var outline = new Outline(target - new XYZ(10,10,10), target + new XYZ(10,10,10));
var nearby = new FilteredElementCollector(doc)
    .WherePasses(new BoundingBoxIntersectsFilter(outline))
    .ToElements();",
                    WhyItMatters = "Revit's native C++ filter eliminates elements before .NET wrapper objects are allocated."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Which class creates a 3D rectangular prism volume for spatial filtering?",
                    Options = new() { "BoundingBoxOutline", "Outline", "CubeFilter", "SpatialBounds" },
                    CorrectOptionIndex = 1,
                    Explanation = "The Outline class defines minimum and maximum XYZ corners for BoundingBoxIntersectsFilter."
                }
            }
        });
        revitModules.Add(modR4);

        // Module R5: The Parameter System Deep Dive
        var modR5 = new Module
        {
            Id = "r-mod-5",
            ModuleCode = "R5",
            TrackId = "revitapi",
            Order = 5,
            LevelNumber = 2,
            LevelTitle = "Stage 2: The Parameter System",
            Title = "Module R5: The Parameter System Deep Dive",
            ShortDescription = "BuiltInParameters, Project/Shared Parameters, StorageTypes, ForgeTypeId, and UnitUtils.",
            Icon = "bi-input-cursor-text",
            Level = DifficultyLevel.Intermediate
        };
        for (int i = 12; i <= 14; i++)
        {
            var tid = $"r-l1-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modR5.Id;
                modR5.Topics.Add(t);
            }
        }
        modR5.Topics.Add(new Topic
        {
            Id = "r-r5-04",
            ModuleId = modR5.Id,
            TrackId = "revitapi",
            Order = 4,
            Title = "Shared Parameters: Programmatic Creation & BindingMap",
            Subtitle = "Creating shared parameters dynamically using DefinitionFile, ExternalDefinitionCreationOptions, and InstanceBinding",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Shared parameters allow custom properties to appear across multi-category schedules and tag families.",
                "Created via doc.Application.OpenSharedParameterFile() and ExternalDefinitionCreationOptions.",
                "Bound to project categories using doc.ParameterBindings.Insert(definition, new InstanceBinding(categorySet))."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The Shared Parameter Pipeline",
                    ContentHtml = "<p>Standardizing company parameters across thousands of models requires code. Manually adding shared parameters takes hours of clicking. With the C# Revit API, you can bind 50 parameters to 30 categories in <strong>under 1 second</strong>.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Binding a Shared Parameter to Walls & Doors",
                    Description = "Full automated shared parameter binding pipeline.",
                    Code = @"// 1. Open shared parameter file
DefinitionFile defFile = doc.Application.OpenSharedParameterFile();
DefinitionGroup group = defFile.Groups.get_Item(""EnterpriseBIM"") ?? defFile.Groups.Create(""EnterpriseBIM"");

// 2. Define parameter spec (Revit 2022+ ForgeTypeId)
var options = new ExternalDefinitionCreationOptions(""BIM_AssetCode"", SpecTypeId.String.Text);
Definition def = group.Definitions.Create(options);

// 3. Create category set for Walls and Doors
CategorySet catSet = doc.Application.Create.NewCategorySet();
catSet.Insert(doc.Settings.Categories.get_Item(BuiltInCategory.OST_Walls));
catSet.Insert(doc.Settings.Categories.get_Item(BuiltInCategory.OST_Doors));

// 4. Bind as Instance parameter
using (var tx = new Transaction(doc, ""Bind Shared Parameter""))
{
    tx.Start();
    var binding = doc.Application.Create.NewInstanceBinding(catSet);
    doc.ParameterBindings.Insert(def, binding, BuiltInParameterGroup.PG_IDENTITY_DATA);
    tx.Commit();
}",
                    Explanation = "Binds BIM_AssetCode to both Walls and Doors with identity data grouping."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Forgetting to Set Shared Parameter File Path",
                    Description = "Calling OpenSharedParameterFile() returns null if no valid file path is configured.",
                    BadCode = @"DefinitionFile file = doc.Application.OpenSharedParameterFile();
file.Groups.Create(""MyGroup""); // NullReferenceException if no shared param file is set!",
                    GoodCode = @"if (string.IsNullOrEmpty(doc.Application.SharedParametersFilename)) {
    doc.Application.SharedParametersFilename = @""C:\BIM\Standards\CompanySharedParams.txt"";
}
DefinitionFile file = doc.Application.OpenSharedParameterFile();",
                    WhyItMatters = "Revit does not default to any shared parameter file path."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Which object binds a parameter definition to categories in a Revit document?",
                    Options = new() { "BindingMap", "ParameterMatrix", "CategoryLinker", "SharedBinder" },
                    CorrectOptionIndex = 0,
                    Explanation = "doc.ParameterBindings is a BindingMap that maps Definitions to Instance or Type Bindings."
                }
            }
        });
        revitModules.Add(modR5);

        // Module R6: Geometry, Vectors & Spatial Math
        var modR6 = new Module
        {
            Id = "r-mod-6",
            ModuleCode = "R6",
            TrackId = "revitapi",
            Order = 6,
            LevelNumber = 3,
            LevelTitle = "Stage 3: Geometry & Spatial Math",
            Title = "Module R6: Geometry, Vectors & Spatial Math",
            ShortDescription = "Revit Coordinates, XYZ vector math, Dot/Cross products, CurveLoops, Solids, Faces, and ReferenceIntersector.",
            Icon = "bi-compass",
            Level = DifficultyLevel.Advanced
        };
        for (int i = 15; i <= 17; i++)
        {
            var tid = $"r-l1-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modR6.Id;
                modR6.Topics.Add(t);
            }
        }
        revitModules.Add(modR6);

        // Module R7: BIM Creation & Modification APIs
        var modR7 = new Module
        {
            Id = "r-mod-7",
            ModuleCode = "R7",
            TrackId = "revitapi",
            Order = 7,
            LevelNumber = 3,
            LevelTitle = "Stage 3: BIM Creation APIs",
            Title = "Module R7: BIM Creation & Modification APIs",
            ShortDescription = "Placing Family Instances, System Elements (Wall.Create, Floors), ElementTransformUtils, MEP, and Views.",
            Icon = "bi-plus-square",
            Level = DifficultyLevel.Advanced
        };
        for (int i = 18; i <= 20; i++)
        {
            var tid = $"r-l1-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modR7.Id;
                modR7.Topics.Add(t);
            }
        }
        revitModules.Add(modR7);

        // Module R8: The Family Creation API
        var modR8 = new Module
        {
            Id = "r-mod-8",
            ModuleCode = "R8",
            TrackId = "revitapi",
            Order = 8,
            LevelNumber = 3,
            LevelTitle = "Stage 3: Family Creation API",
            Title = "Module R8: The Family Creation API",
            ShortDescription = "Family Document vs Project Document, Solid forms (Extrusions, Revolves, Sweeps, Voids), Types, and IFamilyLoadOptions.",
            Icon = "bi-box",
            Level = DifficultyLevel.Advanced
        };
        modR8.Topics.Add(new Topic
        {
            Id = "r-r8-01",
            ModuleId = modR8.Id,
            TrackId = "revitapi",
            Order = 1,
            Title = "Family Document vs Project Document (doc.EditFamily)",
            Subtitle = "Opening, modifying, and reloading .rfa files programmatically in memory",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Advanced,
            KeyTakeaways = new()
            {
                "Revit has two distinct document types: Project Document (.rvt) and Family Document (.rfa).",
                "doc.EditFamily(family) opens the family definition as an active in-memory Document.",
                "Modifications inside the family document require their own dedicated transaction within the family context."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The In-Memory Family Editor Context",
                    ContentHtml = "<p>When editing a family via API, you are modifying a separate <code>Document</code> object. You must start and commit transactions inside the family document, and finally call <code>familyDoc.LoadFamily(projectDoc, new FamilyOption())</code> to push changes back into the project.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Editing Family and Reloading into Project",
                    Description = "Programmatic family modification pattern.",
                    Code = @"// 1. Open family document
Document familyDoc = doc.EditFamily(targetFamily);

// 2. Modify parameters inside family document
using (var tx = new Transaction(familyDoc, ""Update Family Params""))
{
    tx.Start();
    FamilyManager famMgr = familyDoc.FamilyManager;
    var param = famMgr.get_Parameter(""Width"");
    if (param != null)
    {
        famMgr.Set(param, UnitUtils.ConvertToInternalUnits(900, UnitTypeId.Millimeters));
    }
    tx.Commit();
}

// 3. Reload family back into project with overwrite options
familyDoc.LoadFamily(doc, new OverwriteFamilyOptions());
familyDoc.Close(false);",
                    Explanation = "Updates family parameters and pushes definitions directly into the active project."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Trying to Load Family without IFamilyLoadOptions",
                    Description = "Revit silently rejects overwriting parameter values unless IFamilyLoadOptions is supplied.",
                    BadCode = @"familyDoc.LoadFamily(doc); // Fails to overwrite existing type parameter values!",
                    GoodCode = @"public class OverwriteFamilyOptions : IFamilyLoadOptions {
    public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues) {
        overwriteParameterValues = true;
        return true;
    }
    public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues) {
        source = FamilySource.Family;
        overwriteParameterValues = true;
        return true;
    }
}",
                    WhyItMatters = "Without overwriteParameterValues = true, existing project types ignore your updated values."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What method opens a family from a project into an in-memory Document?",
                    Options = new() { "doc.OpenFamily()", "doc.EditFamily()", "doc.ExtractFamily()", "doc.LoadFamily()" },
                    CorrectOptionIndex = 1,
                    Explanation = "doc.EditFamily(family) extracts and opens the family as a Document."
                }
            }
        });
        modR8.Topics.Add(new Topic
        {
            Id = "r-r8-02",
            ModuleId = modR8.Id,
            TrackId = "revitapi",
            Order = 2,
            Title = "Programmatic Solid Forms: Extrusions, Revolves & Sweeps",
            Subtitle = "Generating parametric 3D solids inside family templates using CurveArrArray and SketchPlanes",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Advanced,
            KeyTakeaways = new()
            {
                "Extrusions are created via familyDoc.FamilyCreate.NewExtrusion(isSolid, profile, sketchPlane, end).",
                "Profiles must form closed, non-self-intersecting loops stored in a CurveArrArray.",
                "Void forms cut solid geometry using SolidSolidCutUtils.AddCutBetweenSolids(familyDoc, solid, voidForm)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Form Geometry Generation Engine",
                    ContentHtml = "<p>Creating procedural furniture, structural brackets, or custom mechanical equipment requires generating 3D solid geometry programmatically. The <code>FamilyItemFactory</code> class provides creation methods for Extrusions, Sweeps, Blends, Revolves, and Swept Blends.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Creating a Solid Box Extrusion in Family Document",
                    Description = "Building a 500x500x1000mm parametric extrusion.",
                    Code = @"// Define rectangle profile curves
XYZ p0 = new XYZ(0, 0, 0);
XYZ p1 = new XYZ(2, 0, 0);
XYZ p2 = new XYZ(2, 2, 0);
XYZ p3 = new XYZ(0, 2, 0);

CurveArray curveArray = new CurveArray();
curveArray.Append(Line.CreateBound(p0, p1));
curveArray.Append(Line.CreateBound(p1, p2));
curveArray.Append(Line.CreateBound(p2, p3));
curveArray.Append(Line.CreateBound(p3, p0));

CurveArrArray profile = new CurveArrArray();
profile.Append(curveArray);

// Reference Plane / Sketch Plane
Plane plane = Plane.CreateByNormalAndOrigin(XYZ.BasisZ, XYZ.Zero);
SketchPlane sketchPlane = SketchPlane.Create(familyDoc, plane);

// Generate Solid Extrusion
using (var tx = new Transaction(familyDoc, ""Create Extrusion""))
{
    tx.Start();
    Extrusion box = familyDoc.FamilyCreate.NewExtrusion(true, profile, sketchPlane, 4.0);
    tx.Commit();
}",
                    Explanation = "Generates a true native Revit solid extrusion that responds to family parameters."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Unclosed Curve Loops in Profile",
                    Description = "Revit throws an InternalException or CurveArrayNotClosedException if profile endpoints do not match exactly.",
                    BadCode = @"// Missing the closure segment from p3 to p0!
curveArray.Append(Line.CreateBound(p0, p1));
curveArray.Append(Line.CreateBound(p1, p2));
curveArray.Append(Line.CreateBound(p2, p3));",
                    GoodCode = @"// Verify end of each curve matches start of next:
curveArray.Append(Line.CreateBound(p3, p0)); // Complete the loop!",
                    WhyItMatters = "All Revit boundary profiles require mathematically closed loops."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What factory property on a family document creates new solid forms?",
                    Options = new() { "familyDoc.Create", "familyDoc.FamilyCreate", "familyDoc.GeometryFactory", "familyDoc.SolidMaker" },
                    CorrectOptionIndex = 1,
                    Explanation = "familyDoc.FamilyCreate (FamilyItemFactory) generates Extrusions, Revolves, and Sweeps."
                }
            }
        });
        revitModules.Add(modR8);

        // Module R9: UI, MVVM & Dockable Panels
        var modR9 = new Module
        {
            Id = "r-mod-9",
            ModuleCode = "R9",
            TrackId = "revitapi",
            Order = 9,
            LevelNumber = 4,
            LevelTitle = "Stage 4: UI, MVVM & Dockable Panels",
            Title = "Module R9: UI, MVVM & Dockable Panels",
            ShortDescription = "WPF & XAML, MVVM architecture, RelayCommand, Dockable Panes, Model Navigator & Room Wizard.",
            Icon = "bi-columns-gap",
            Level = DifficultyLevel.Advanced
        };
        // Topics r-m-01 to r-m-20
        for (int i = 1; i <= 20; i++)
        {
            var tid = $"r-m-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modR9.Id;
                modR9.Topics.Add(t);
            }
        }
        revitModules.Add(modR9);

        // Module R10: Advanced Architecture, Async & Multi-Threading
        var modR10 = new Module
        {
            Id = "r-mod-10",
            ModuleCode = "R10",
            TrackId = "revitapi",
            Order = 10,
            LevelNumber = 4,
            LevelTitle = "Stage 4: Advanced Architecture & DMU",
            Title = "Module R10: Advanced Architecture, Async & Multi-Threading",
            ShortDescription = "Modeless dialogs with ExternalEvent, Dynamic Model Update (DMU), DocumentChanged events, Extensible Storage, and CI/CD.",
            Icon = "bi-shield-check",
            Level = DifficultyLevel.Expert
        };
        // Topics r-m-21 to r-m-31
        for (int i = 21; i <= 31; i++)
        {
            var tid = $"r-m-{i:D2}";
            if (allRawTopics.TryGetValue(tid, out var t))
            {
                t.ModuleId = modR10.Id;
                modR10.Topics.Add(t);
            }
        }
        revitModules.Add(modR10);
    }
}
