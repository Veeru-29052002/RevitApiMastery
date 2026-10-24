using RevitApiMastery.Models;

namespace RevitApiMastery.Services;

public partial class CurriculumService
{
    private Module BuildRevitApiLevel01Module()
    {
        var mod = new Module
        {
            Id = "revit-lvl01",
            TrackId = "revitapi",
            Order = 1,
            LevelNumber = 3,
            LevelTitle = "Stage 3: Revit API Level 01 (Core Engine)",
            Title = "Revit API Stage 3: Core Architecture & Database",
            ShortDescription = "Solution setup, RevitLookup, Ribbon UI, Transactions, Collectors, Parameters, Geometry math, and Element Creation.",
            Icon = "bi-boxes",
            Level = DifficultyLevel.Intermediate
        };

        // 42. Creating Solution
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-01",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 1,
            Title = "Creating a Revit C# Solution",
            Subtitle = "Project configuration, SDK references, and multi-version target setup",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Create a C# 'Class Library' project in Visual Studio.",
                "Reference RevitAPI.dll and RevitAPIUI.dll from Revit's install directory.",
                "Always set <Private>False</Private> (Copy Local = False) on Revit references.",
                "Revit 2021-2024 targets .NET Framework 4.8; Revit 2025+ targets .NET 8 (net8.0-windows)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Modern SDK-Style .csproj for Revit",
                    ContentHtml = "<p>Modern SDK-style projects are vastly cleaner than legacy XML. You can enable WPF and configure references cleanly in just a few lines.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Revit 2025 SDK-Style .csproj",
                    Description = "Modern .csproj targeting Revit 2025 with .NET 8.",
                    Code = @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <Reference Include=""RevitAPI"">
      <HintPath>C:\Program Files\Autodesk\Revit 2025\RevitAPI.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""RevitAPIUI"">
      <HintPath>C:\Program Files\Autodesk\Revit 2025\RevitAPIUI.dll</HintPath>
      <Private>False</Private>
    </Reference>
  </ItemGroup>
</Project>",
                    Explanation = "Setting <Private>False</Private> ensures you don't copy Autodesk's 100MB+ binaries into your output folder."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Copy Local = True",
                    Description = "Copying RevitAPI.dll to your output directory causes DLL version lock conflicts and crashes.",
                    BadCode = @"<Reference Include=""RevitAPI""><Private>True</Private></Reference>",
                    GoodCode = @"<Reference Include=""RevitAPI""><Private>False</Private></Reference>",
                    WhyItMatters = "Revit already loads its own API DLLs in memory at startup."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What project type should you select in Visual Studio when starting a Revit add-in?",
                    Options = new() { "Console Application", "Class Library", "WPF Application (.exe)", "Windows Forms App" },
                    CorrectOptionIndex = 1,
                    Explanation = "Revit plugins are compiled as dynamic link libraries (.dll) using a Class Library project."
                }
            }
        });

        // 43. Debug Components
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-02",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 2,
            Title = "Debugging Revit Add-ins in Visual Studio",
            Subtitle = "Attaching to Revit.exe process, breakpoints, edit-and-continue, and hot debugging",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Configure your project's Debug Start Action to launch 'Revit.exe' directly.",
                "Or start Revit manually and use Visual Studio: Debug > Attach to Process (select 'Revit.exe').",
                "Hit breakpoints inside your Execute() method to step through parameters and transactions line by line."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Setting Debug Target in Visual Studio",
                    ContentHtml = "<ol><li>Right-click Project > <strong>Properties</strong> > <strong>Debug</strong>.</li><li>Click <strong>Open debug launch profiles UI</strong>.</li><li>Set <strong>Executable Path</strong> to: <code>C:\\Program Files\\Autodesk\\Revit 2025\\Revit.exe</code>.</li><li>Press <strong>F5</strong> to launch Revit with the debugger attached automatically!</li></ol>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "launchSettings.json for Direct F5 Debugging",
                    Description = "Automatic debugger configuration.",
                    Code = @"{
  ""profiles"": {
    ""Revit 2025"": {
      ""commandName"": ""Executable"",
      ""executablePath"": ""C:\\Program Files\\Autodesk\\Revit 2025\\Revit.exe""
    }
  }
}",
                    Explanation = "Allows any developer on the team to press F5 to launch Revit with debugging."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Debugging in Release Mode",
                    Description = "Debugging with the 'Release' configuration causes breakpoint warnings ('The breakpoint will not currently be hit').",
                    BadCode = @"// Configuration: Release (Compiler optimizes away variables and lines)",
                    GoodCode = @"// Configuration: Debug (Full PDB symbol generation)",
                    WhyItMatters = "Debug builds maintain exact code-to-line mappings for inspection."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What process name do you attach the Visual Studio debugger to when debugging an add-in?",
                    Options = new() { "devenv.exe", "Revit.exe", "msbuild.exe", "explorer.exe" },
                    CorrectOptionIndex = 1,
                    Explanation = "Your compiled DLL is executed inside the Revit.exe host process."
                }
            }
        });

        // 44. Revit API Documentation
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-03",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 3,
            Title = "Revit API Documentation & RevitAPIDocs",
            Subtitle = "Navigating RevitAPIDocs.com, the official SDK CHM file, and api-changes guides",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "RevitAPIDocs.com is the definitive community search engine for all Revit API classes across multiple versions.",
                "The official SDK includes 'RevitAPI.chm' located in your SDK installation folder.",
                "Always consult 'What's New in the Revit API' to track deprecated methods between releases."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Essential Documentation Resources",
                    ContentHtml = "<ul><li><a href='https://www.revitapidocs.com' target='_blank'><strong>RevitAPIDocs.com</strong></a>: Fast class and member index with version diffing.</li><li><strong>The Building Coder (Jeremy Tammik)</strong>: Deep industrial blog exploring edge cases and solutions.</li><li><strong>Autodesk Developer Network (ADN)</strong>: Official guides and sample code.</li></ul>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Checking Obsolete API Annotations",
                    Description = "Recognizing deprecation warnings in compiler output.",
                    Code = @"// Example of Deprecated API:
// [Obsolete(""This method is deprecated in Revit 2021. Use UnitUtils.ConvertFromInternalUnits with ForgeTypeId instead."")]
// UnitUtils.ConvertFromInternalUnits(val, DisplayUnitType.DUT_MILLIMETERS);

// Modern Supported Equivalent:
UnitUtils.ConvertFromInternalUnits(val, UnitTypeId.Millimeters);",
                    Explanation = "Compiler warnings indicate when an API method is slated for removal in future versions."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Using Obsolete Code from Old Forum Posts",
                    Description = "Copying 2012-era forum snippets that use deprecated classes like Document.get_Elements() or DisplayUnitType.",
                    BadCode = @"// Legacy code from 2014 forum:
ElementIterator iter = doc.Elements; // Removed in Revit 2016!",
                    GoodCode = @"// Modern code:
FilteredElementCollector collector = new FilteredElementCollector(doc);",
                    WhyItMatters = "Legacy code will fail to compile in modern Revit versions."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What online documentation website is most popular among Revit API developers?",
                    Options = new() { "RevitAPIDocs.com", "W3Schools", "MDN Web Docs", "Stack Overflow Documentation" },
                    CorrectOptionIndex = 0,
                    Explanation = "RevitAPIDocs.com provides an indexed, searchable mirror of the official Revit API SDK documentation."
                }
            }
        });

        // 45. Ribbon Components
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-04",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 4,
            Title = "Ribbon Components: Custom Tabs & PushButtons",
            Subtitle = "Creating custom tabs, panels, push buttons, split buttons, icons, and tooltips in OnStartup",
            ReadTimeMinutes = 10,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Custom ribbons are created inside IExternalApplication.OnStartup(UIControlledApplication).",
                "Create tabs with application.CreateRibbonTab(tabName) and panels with application.CreateRibbonPanel().",
                "PushButtons require an assembly path, target command class name, and 32x32 / 16x16 PNG icons.",
                "Support SplitButtons, PulldownButtons, and RadioButtonGroup."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Loading Embedded PNG Icons",
                    ContentHtml = "<p>Store your button icons as <strong>Resource</strong> in your project, and load them using WPF's <code>pack://application:,,,/AssemblyName;component/Path/icon.png</code> syntax.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Complete Ribbon Panel with PushButton",
                    Description = "Standard industrial ribbon creation in App.cs.",
                    Code = @"using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

public class App : IExternalApplication
{
    public Result OnStartup(UIControlledApplication app)
    {
        string tab = ""BIM Tools"";
        app.CreateRibbonTab(tab);
        RibbonPanel panel = app.CreateRibbonPanel(tab, ""Auditing"");

        string dll = Assembly.GetExecutingAssembly().Location;
        PushButtonData btnData = new PushButtonData(
            ""btnAudit"", 
            ""Audit\nModel"", 
            dll, 
            ""BimTools.AuditCommand"")
        {
            ToolTip = ""Validates wall parameters and dimensions."",
            LargeImage = new BitmapImage(new Uri(""pack://application:,,,/BimTools;component/Resources/audit32.png""))
        };

        panel.AddItem(btnData);
        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication app) => Result.Succeeded;
}",
                    Explanation = "The full lifecycle of custom Revit UI integration."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Creating Duplicate Ribbon Tabs",
                    Description = "Calling CreateRibbonTab with a tab name that already exists throws an Autodesk.Revit.Exceptions.ArgumentException.",
                    BadCode = @"app.CreateRibbonTab(tabName); // Crashes if tab already exists!",
                    GoodCode = @"try {
    app.CreateRibbonTab(tabName);
} catch (Autodesk.Revit.Exceptions.ArgumentException) {
    // Tab already exists; proceed to add panel
}",
                    WhyItMatters = "Prevents crashes when multiple add-ins share the same organizational tab."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "In which lifecycle method should custom Revit Ribbon UI tabs and panels be registered?",
                    Options = new() { "IExternalCommand.Execute()", "IExternalApplication.OnStartup()", "IExternalApplication.OnShutdown()", "In the .csproj file" },
                    CorrectOptionIndex = 1,
                    Explanation = "Ribbon panels must be created during application startup (OnStartup) before any documents are opened."
                }
            }
        });

        // 46. Revit Lookup Plugin
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-05",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 5,
            Title = "RevitLookup: The X-Ray Machine of BIM Developers",
            Subtitle = "Installing, mastering, and inspecting elements, parameters, and geometry in real-time",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "RevitLookup is the #1 most important developer tool for Autodesk Revit.",
                "Allows selecting any element in the active view and inspecting its internal C++ properties, parameters, and geometry.",
                "Shows exact parameter BuiltInParameter enums, StorageTypes, and internal unit values.",
                "Available as an open-source tool maintained by the Revit community."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "How to Use RevitLookup to Write Code",
                    ContentHtml = "<ol><li>Select an element in Revit (e.g. an exterior wall).</li><li>Click <strong>RevitLookup &gt; Snooze Selection</strong>.</li><li>Inspect the <strong>Parameters</strong> collection: identify whether the parameter is Type or Instance, and view its exact <code>BuiltInParameter</code>.</li><li>Inspect <strong>GeometryElement</strong>: explore its Solids, Faces, and Edges.</li></ol>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Verifying What RevitLookup Shows in C#",
                    Description = "Reading the exact parameter revealed by RevitLookup.",
                    Code = @"// If RevitLookup shows:
// Parameter: ""Unconnected Height"" -> BuiltInParameter: WALL_USER_HEIGHT_PARAM
// StorageType: Double

Parameter? param = wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
if (param != null)
{
    double internalFeet = param.AsDouble();
    double meters = UnitUtils.ConvertFromInternalUnits(internalFeet, UnitTypeId.Meters);
    System.Console.WriteLine($""Wall height: {meters:F2} m"");
}",
                    Explanation = "Translates visual inspection directly into robust code."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Guessing Parameter Names Instead of Looking Up",
                    Description = "Writing code guessing that a parameter is named 'Height' when the internal database name is WALL_USER_HEIGHT_PARAM.",
                    BadCode = @"var p = elem.LookupParameter(""Height""); // Returns null!",
                    GoodCode = @"var p = elem.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM); // Guaranteed match",
                    WhyItMatters = "RevitLookup reveals exact internal database keys."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What is the primary role of the RevitLookup tool?",
                    Options = new() { "It renders realistic ray-traced images", "It inspects and exposes the internal database properties, parameters, and geometry of Revit elements", "It audits employee working hours", "It converts Revit files to PDF" },
                    CorrectOptionIndex = 1,
                    Explanation = "RevitLookup lets developers inspect the internal database representation of any Revit object in real-time."
                }
            }
        });

        // 47. Application and Document
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-06",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 6,
            Title = "Application & Document Hierarchy",
            Subtitle = "UIApplication, Application, UIDocument, Document, and ActiveView",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "UIApplication / Application: Represents the overall Autodesk Revit session, version, and global settings.",
                "UIDocument / Document: Represents the active project / family file.",
                "UIDocument handles UI interactions (active view, selection, dialogs); Document handles database modifications.",
                "doc.ActiveView gives access to the currently displayed 2D/3D view."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The Two Parallel Hierarchies",
                    ContentHtml = "<table class='table table-dark table-sm'><thead><tr><th>UI Layer (RevitAPIUI.dll)</th><th>Database Layer (RevitAPI.dll)</th></tr></thead><tbody><tr><td><code>UIApplication</code></td><td><code>Application</code></td></tr><tr><td><code>UIDocument</code></td><td><code>Document</code></td></tr><tr><td><code>Selection</code></td><td><code>FilteredElementCollector</code></td></tr></tbody></table>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Navigating from ExternalCommandData",
                    Description = "Extracting UI and DB handles in a command.",
                    Code = @"public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
{
    // Top-level UI context:
    UIApplication uiapp = commandData.Application;
    UIDocument uidoc = uiapp.ActiveUIDocument;

    // In-memory Database context:
    Document doc = uidoc.Document;

    // Active View:
    View activeView = doc.ActiveView;

    System.Console.WriteLine($""Project: {doc.Title} | View: {activeView.Name}"");
    return Result.Succeeded;
}",
                    Explanation = "The standard opening sequence for any IExternalCommand."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Calling UI Methods on Background Threads",
                    Description = "Accessing UIDocument.Selection on a secondary thread throws an InvalidOperationException.",
                    BadCode = @"Task.Run(() => { var sel = uidoc.Selection; }); // FATAL CRASH!",
                    GoodCode = @"// Query selection strictly on the main thread inside Execute()",
                    WhyItMatters = "UIDocument requires Revit UI thread affinity."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Which object represents the database of elements, materials, and transactions in a Revit project?",
                    Options = new() { "UIApplication", "UIDocument", "Document", "ExternalCommandData" },
                    CorrectOptionIndex = 2,
                    Explanation = "The 'Document' class represents the project or family database containing all BIM elements and transactions."
                }
            }
        });

        // 48. Element and Element Type
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-07",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 7,
            Title = "Element vs ElementType: Instance vs Type",
            Subtitle = "Understanding the fundamental distinction between placed instances and their type definitions",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "An ElementType (or FamilySymbol) defines properties shared by ALL elements of that type (e.g. wall thickness, thermal rating).",
                "An Element instance defines properties specific to ONE placed instance (e.g. location, length, mark).",
                "Changing a parameter on an ElementType updates every single placed instance in the entire building!"
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Retrieving an Element's Type",
                    ContentHtml = "<p>To get the ElementType of an element instance: <code>ElementType type = doc.GetElement(elem.GetTypeId()) as ElementType;</code></p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Instance vs Type Parameter Access",
                    Description = "Accessing both instance and type parameters from a wall.",
                    Code = @"public void InspectWall(Wall wall, Document doc)
{
    // 1. Instance parameter (unique to this wall):
    double length = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH)?.AsDouble() ?? 0.0;

    // 2. Type parameter (shared by all walls of this type):
    ElementId typeId = wall.GetTypeId();
    WallType? wallType = doc.GetElement(typeId) as WallType;
    double width = wallType?.get_Parameter(BuiltInParameter.WALL_ATTR_WIDTH_PARAM)?.AsDouble() ?? 0.0;

    System.Console.WriteLine($""Wall Length: {length:F2} ft | Wall Thickness: {width:F2} ft"");
}",
                    Explanation = "Demonstrates navigating from instance to type definition."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Modifying Type Parameter Expecting Instance Change",
                    Description = "Setting a parameter on an ElementType when intending to modify only one element modifies every other element in the model.",
                    BadCode = @"// DANGEROUS: Changes type mark for every door in the building!
door.Symbol.LookupParameter(""Type Mark"").Set(""D-01"");",
                    GoodCode = @"// Correct: Modify instance parameter for this single door:
door.LookupParameter(""Mark"").Set(""D-01"");",
                    WhyItMatters = "Type modifications propagate globally across the entire project."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What method retrieves the ElementType ID for a given Element instance?",
                    Options = new() { "elem.GetType()", "elem.GetTypeId()", "elem.Id", "elem.ParentId" },
                    CorrectOptionIndex = 1,
                    Explanation = "elem.GetTypeId() returns the ElementId of the ElementType/FamilySymbol that defines this instance."
                }
            }
        });

        // 49. BuiltInCategory
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-08",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 8,
            Title = "BuiltInCategory: The Master Category Enum",
            Subtitle = "OST_Walls, OST_Doors, OST_Windows, OST_Rooms, and filtering categories",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "BuiltInCategory is a massive enum containing every built-in category in Autodesk Revit.",
                "Every built-in category begins with the prefix 'OST_' (e.g. OST_Walls, OST_Doors, OST_StructuralFraming).",
                "Using BuiltInCategory is completely language-independent and works across English, French, and Japanese versions of Revit."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Category Comparison in C#",
                    ContentHtml = "<p>To check if an element belongs to a category:</p><p><code>bool isWall = elem.Category?.Id.IntegerValue == (int)BuiltInCategory.OST_Walls;</code></p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Filtering with BuiltInCategory",
                    Description = "Native C++ category filtering with collector.",
                    Code = @"// Quick-filter elements by BuiltInCategory
var doorsCollector = new FilteredElementCollector(doc)
    .OfCategory(BuiltInCategory.OST_Doors)
    .WhereElementIsNotElementType();

System.Console.WriteLine($""Total door instances: {doorsCollector.GetElementCount()}"");",
                    Explanation = "OfCategory executes inside native memory before any C# proxies are instantiated."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Category Null Checks",
                    Description = "Some Revit internal elements (like Extensible Storage schemas or internal settings) have a null Category.",
                    BadCode = @"if (elem.Category.Name == ""Walls"") { /* Crashes if Category is null! */ }",
                    GoodCode = @"if (elem.Category != null && elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Walls) { /* Safe */ }",
                    WhyItMatters = "Prevents NullReferenceException when scanning raw model elements."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What prefix do all BuiltInCategory enum values start with in the Revit API?",
                    Options = new() { "CAT_", "REVIT_", "OST_", "BIM_" },
                    CorrectOptionIndex = 2,
                    Explanation = "All built-in categories in Revit begin with 'OST_' (e.g. BuiltInCategory.OST_Walls)."
                }
            }
        });

        // 50. Filter Element Collector
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-09",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 9,
            Title = "FilteredElementCollector: Querying the Database",
            Subtitle = "The fundamental query engine: Quick Filters, Slow Filters, and LINQ execution",
            ReadTimeMinutes = 11,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "FilteredElementCollector is the only legitimate way to query elements from the Revit database.",
                "Always chain QuickFilters (OfCategory, OfClass, WhereElementIsNotElementType) first.",
                "Scope queries to the active view if you only need elements visible on screen.",
                "Use ToElementIds() instead of ToElements() if you only need IDs (3x-5x faster)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Performance Optimization Rules",
                    ContentHtml = "<ol><li><strong>Pass ViewId</strong> when searching visible geometry: <code>new FilteredElementCollector(doc, viewId)</code>.</li><li><strong>Filter non-elements</strong>: always add <code>.WhereElementIsNotElementType()</code> to exclude type definitions.</li><li><strong>Extract IDs first</strong>: <code>.ToElementIds()</code> avoids loading managed element wrappers.</li></ol>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "High-Performance Collector Query",
                    Description = "Idiomatic chained collector query.",
                    Code = @"// Fetch non-type wall instances in the project:
var walls = new FilteredElementCollector(doc)
    .OfCategory(BuiltInCategory.OST_Walls)
    .OfClass(typeof(Wall))
    .WhereElementIsNotElementType()
    .Cast<Wall>()
    .ToList();",
                    Explanation = "Filters execute natively in C++ before casting to Wall instances."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Forgetting WhereElementIsNotElementType()",
                    Description = "Omitting this filter returns both Wall instances and WallTypes, corrupting instance calculations.",
                    BadCode = @"var walls = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).ToElements();",
                    GoodCode = @"var walls = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements();",
                    WhyItMatters = "Wall types do not possess locations, geometry instances, or lengths."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What method should you call on a FilteredElementCollector to exclude FamilySymbols and ElementTypes?",
                    Options = new() { "WhereElementIsNotElementType()", "ExcludeTypes()", "InstancesOnly()", "WherePasses(TypeFilter)" },
                    CorrectOptionIndex = 0,
                    Explanation = "WhereElementIsNotElementType() is the native quick filter that filters out all types."
                }
            }
        });

        // 51. Transaction
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-10",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 10,
            Title = "Transaction: The ACID Engine",
            Subtitle = "Start, Commit, RollBack, using blocks, and managing database state",
            ReadTimeMinutes = 11,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Modifying any Revit element or parameter requires an open Transaction.",
                "Always wrap Transactions in a C# 'using' statement to ensure rollback on failure.",
                "Only one Transaction can be open on a document at any time.",
                "Transactions require a descriptive user-friendly name (shown in Revit's Undo menu)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Transaction Lifecycle in C#",
                    ContentHtml = "<p>The canonical structure: <code>using (var t = new Transaction(doc, \"Name\")) { t.Start(); ... t.Commit(); }</code>. If an unhandled exception occurs inside the braces, <code>Dispose()</code> automatically rolls back the transaction.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Canonical Transaction Template",
                    Description = "Modifying element parameters transactionally.",
                    Code = @"using (Transaction trans = new Transaction(doc, ""Update Wall Comments""))
{
    trans.Start();

    foreach (var wall in walls)
    {
        wall.LookupParameter(""Comments"")?.Set(""Verified by BIM Tool"");
    }

    // Commits all changes to the database
    TransactionStatus status = trans.Commit();
    if (status != TransactionStatus.Committed)
    {
        // Handle failure...
    }
}",
                    Explanation = "Guarantees database consistency."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "ModificationForbiddenException",
                    Description = "Attempting to modify a parameter or delete an element outside an active transaction.",
                    BadCode = @"elem.LookupParameter(""Comments"").Set(""test""); // CRASH: No active transaction!",
                    GoodCode = @"using (var t = new Transaction(doc, ""Set"")) { t.Start(); elem.LookupParameter(""Comments"")?.Set(""test""); t.Commit(); }",
                    WhyItMatters = "Revit enforces strict ACID transactional database properties."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What happens if a Transaction object exits its 'using' block without calling trans.Commit()?",
                    Options = new() { "Revit automatically commits the changes", "The transaction automatically rolls back all changes", "Revit crashes with a fatal error", "The document is locked" },
                    CorrectOptionIndex = 1,
                    Explanation = "If Commit() is not called before disposal, Transaction.Dispose() rolls back all database changes."
                }
            }
        });

        // 52. Transaction Group
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-11",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 11,
            Title = "Transaction Group: Multi-Transaction Undo",
            Subtitle = "Assimilate, RollBack, and grouping multiple operations into a single Undo entry",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "TransactionGroup combines multiple consecutive Transactions into a single Undo action.",
                "Call group.Assimilate() to merge all sub-transactions into one Undo history item.",
                "Call group.RollBack() to undo all nested transactions in one clean sweep."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Why Use Transaction Groups?",
                    ContentHtml = "<p>If your tool performs 5 separate operations (creates walls, creates doors, tags doors), without a TransactionGroup the user has to press <strong>Ctrl+Z five times</strong> to undo! With <code>group.Assimilate()</code>, pressing Ctrl+Z once undoes the entire command.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "TransactionGroup with Assimilate()",
                    Description = "Grouping multiple transactions into a single Undo item.",
                    Code = @"using (TransactionGroup group = new TransactionGroup(doc, ""Batch Renumber Doors""))
{
    group.Start();

    // Step 1: Sub-transaction
    using (Transaction t1 = new Transaction(doc, ""Phase 1""))
    {
        t1.Start();
        // updates...
        t1.Commit();
    }

    // Step 2: Sub-transaction
    using (Transaction t2 = new Transaction(doc, ""Phase 2""))
    {
        t2.Start();
        // updates...
        t2.Commit();
    }

    // Merges t1 and t2 into a single 'Batch Renumber Doors' undo item!
    group.Assimilate();
}",
                    Explanation = "Provides a professional user experience matching native Revit commands."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Calling Commit() on TransactionGroup Instead of Assimilate()",
                    Description = "Calling group.Commit() keeps all sub-transactions as separate Undo items instead of merging them.",
                    BadCode = @"group.Commit(); // Leaves 10 individual undo items in Revit Edit menu",
                    GoodCode = @"group.Assimilate(); // Cleanly merges into 1 undo item",
                    WhyItMatters = "Assimilate() provides the expected single-step undo behavior."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What method on TransactionGroup merges all nested sub-transactions into a single Undo entry?",
                    Options = new() { "group.Commit()", "group.Assimilate()", "group.Merge()", "group.Combine()" },
                    CorrectOptionIndex = 1,
                    Explanation = "TransactionGroup.Assimilate() combines all committed sub-transactions into a single Undo action in Revit."
                }
            }
        });

        // 53. Selections
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-12",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 12,
            Title = "Selections: Interactive User Picking",
            Subtitle = "PickObject, PickObjects, PickElementsByRectangle, and reading active selection",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "UIDocument.Selection manages interactive user selection.",
                "uidoc.Selection.GetElementIds() gets elements selected before running the command.",
                "uidoc.Selection.PickObject(ObjectType.Element) prompts the user to click an element.",
                "Always catch OperationCanceledException when user presses ESC to cancel picking."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Active Selection vs Interactive Prompt",
                    ContentHtml = "<p>Best practice: first check if the user already selected elements (<code>GetElementIds()</code>). If empty, prompt them with <code>PickObjects()</code>.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Hybrid Selection Workflow",
                    Description = "Checks active selection or prompts user to pick elements.",
                    Code = @"var uidoc = commandData.Application.ActiveUIDocument;
ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();

if (selectedIds.Count == 0)
{
    // User hasn't pre-selected: prompt to pick multiple elements
    IList<Reference> pickedRefs = uidoc.Selection.PickObjects(
        ObjectType.Element, 
        ""Select elements to audit"");

    selectedIds = pickedRefs.Select(r => r.ElementId).ToList();
}

System.Console.WriteLine($""Processing {selectedIds.Count} elements"");",
                    Explanation = "Supports both pre-selection and post-selection seamlessly."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Not Catching OperationCanceledException",
                    Description = "Pressing ESC during PickObject throws OperationCanceledException.",
                    BadCode = @"Reference r = uidoc.Selection.PickObject(ObjectType.Element); // Crashes if user presses ESC!",
                    GoodCode = @"try {
    Reference r = uidoc.Selection.PickObject(ObjectType.Element);
} catch (Autodesk.Revit.Exceptions.OperationCanceledException) {
    return Result.Cancelled;
}",
                    WhyItMatters = "ESC is the standard Revit shortcut to cancel any pick prompt."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What method prompts the user to select an element by clicking it in the Revit viewport?",
                    Options = new() { "doc.GetElement()", "uidoc.Selection.PickObject()", "uidoc.Select()", "Window.Show()" },
                    CorrectOptionIndex = 1,
                    Explanation = "UIDocument.Selection.PickObject() activates interactive picking mode in the Revit viewport."
                }
            }
        });

        // 54. Filter Selection
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-13",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 13,
            Title = "Filter Selection: ISelectionFilter Mastery",
            Subtitle = "Restricting user picking strictly to walls, doors, rooms, or specific faces",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "ISelectionFilter constrains interactive viewport selection.",
                "AllowElement(Element elem) returns true if the element can be hovered/clicked.",
                "AllowReference(Reference r, XYZ pt) filters geometric sub-elements like planar faces.",
                "Prevents user errors by making non-target elements unclickable."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Creating a Category-Specific Picker",
                    ContentHtml = "<p>Pass your custom <code>ISelectionFilter</code> into <code>uidoc.Selection.PickObject(ObjectType.Element, filter)</code>. Elements not matching your filter cannot be hovered or clicked.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Dedicated Wall Selection Filter",
                    Description = "Restricts mouse picks strictly to Wall elements.",
                    Code = @"using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

public class WallOnlySelectionFilter : ISelectionFilter
{
    public bool AllowElement(Element elem) => elem is Wall;
    public bool AllowReference(Reference reference, XYZ position) => false;
}

// In your command:
var filter = new WallOnlySelectionFilter();
Reference pickedRef = uidoc.Selection.PickObject(ObjectType.Element, filter, ""Click on any Wall"");",
                    Explanation = "Revit highlights ONLY walls when the cursor hovers over the model."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Returning True for All References",
                    Description = "Returning true on AllowReference when ObjectType is Element causes unexpected reference selections.",
                    BadCode = @"public bool AllowReference(Reference r, XYZ p) => true; // Misleading for element picks",
                    GoodCode = @"public bool AllowReference(Reference r, XYZ p) => false;",
                    WhyItMatters = "AllowReference should only return true when picking faces or edges."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What method on ISelectionFilter determines whether an element in the model can be clicked by the user?",
                    Options = new() { "CanSelect()", "AllowElement(Element elem)", "IsValidElement()", "FilterPasses()" },
                    CorrectOptionIndex = 1,
                    Explanation = "AllowElement(Element elem) returns a boolean indicating whether the cursor can click that element."
                }
            }
        });

        // 55. Parameters
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-14",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 14,
            Title = "Parameters: Reading, Writing & Safe Access",
            Subtitle = "LookupParameter, get_Parameter, StorageType, IsReadOnly, and parameter transactions",
            ReadTimeMinutes = 11,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Parameters store all building metadata (dimensions, descriptions, fire ratings).",
                "LookupParameter('Comments') finds parameters by name; get_Parameter(BuiltInParameter) finds by enum.",
                "Always verify param != null && !param.IsReadOnly before calling param.Set().",
                "Numbers are ALWAYS stored in Revit Imperial internal units (Feet, Sq.Ft, Cu.Ft)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Safe Value Setting by StorageType",
                    ContentHtml = "<table class='table table-dark table-sm'><thead><tr><th>StorageType</th><th>Setter Method</th><th>Getter Method</th></tr></thead><tbody><tr><td>String</td><td><code>param.Set(string)</code></td><td><code>param.AsString()</code></td></tr><tr><td>Double</td><td><code>param.Set(double)</code></td><td><code>param.AsDouble()</code></td></tr><tr><td>Integer</td><td><code>param.Set(int)</code></td><td><code>param.AsInteger()</code></td></tr><tr><td>ElementId</td><td><code>param.Set(ElementId)</code></td><td><code>param.AsElementId()</code></td></tr></tbody></table>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Safe Parameter Writing Routine",
                    Description = "Checking read-only status before writing.",
                    Code = @"public bool TryUpdateComment(Element elem, string newComment)
{
    Parameter? param = elem.LookupParameter(""Comments"");
    if (param == null || param.IsReadOnly)
        return false;

    return param.Set(newComment);
}",
                    Explanation = "Prevents crashes on read-only or calculated parameters."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Calling AsString() on Number Parameters",
                    Description = "Calling .AsString() on a Double or Integer parameter returns null (you must use AsDouble() or AsValueString()).",
                    BadCode = @"string val = wall.LookupParameter(""Length"").AsString(); // NULL! Length is a Double!",
                    GoodCode = @"double feet = wall.LookupParameter(""Length"").AsDouble(); // Correct
string formatted = wall.LookupParameter(""Length"").AsValueString(); // '30.00 m'",
                    WhyItMatters = "AsString() only works on StorageType.String."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What method returns the formatted text string of a numeric parameter as seen in the Revit Properties palette (e.g. '350 mm')?",
                    Options = new() { "param.AsString()", "param.AsValueString()", "param.ToString()", "param.Display()" },
                    CorrectOptionIndex = 1,
                    Explanation = "param.AsValueString() formats the underlying numeric value using the project's active display units."
                }
            }
        });

        // 56. XYZ and Point Geometry
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-15",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 15,
            Title = "XYZ and Point Geometry: 3D Coordinates",
            Subtitle = "XYZ coordinates, DistanceTo, IsAlmostEqualTo, BasisX/Y/Z, and origin vectors",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "XYZ represents a point or vector in 3D Cartesian coordinates (X, Y, Z in decimal feet).",
                "Common constants: XYZ.Zero (0,0,0), XYZ.BasisX (1,0,0), XYZ.BasisY (0,1,0), XYZ.BasisZ (0,0,1).",
                "point1.DistanceTo(point2) computes Euclidean distance.",
                "XYZ is an immutable reference type (class) in Revit API."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Vector vs Point in XYZ",
                    ContentHtml = "<p>The <code>XYZ</code> class is used interchangeably as both a <strong>Point</strong> (a location in space) and a <strong>Vector</strong> (a direction with magnitude). Subtracting two points yields a displacement vector: <code>XYZ vector = pt2 - pt1;</code></p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Working with XYZ Points and Vectors",
                    Description = "Distance and midpoint calculation.",
                    Code = @"XYZ startPoint = new XYZ(0, 0, 0);
XYZ endPoint = new XYZ(10, 0, 0);

// Distance between points:
double lengthFeet = startPoint.DistanceTo(endPoint);

// Midpoint calculation:
XYZ midpoint = (startPoint + endPoint) / 2.0;

// Direction vector:
XYZ direction = (endPoint - startPoint).Normalize();

System.Console.WriteLine($""Midpoint: ({midpoint.X}, {midpoint.Y}, {midpoint.Z})"");",
                    Explanation = "Standard vector arithmetic using C# operator overloads (+, -, /)."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Attempting to Mutate XYZ Coordinates",
                    Description = "XYZ is immutable; trying to assign 'pt.X = 10;' causes compiler error CS0200.",
                    BadCode = @"XYZ pt = new XYZ(0, 0, 0);
pt.X = 10; // COMPILE ERROR: Property 'X' is read-only!",
                    GoodCode = @"XYZ pt = new XYZ(10, 0, 0); // Instantiate new vector",
                    WhyItMatters = "Immutability guarantees that shared vectors cannot be accidentally modified."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What unit of measurement are X, Y, and Z values in an Autodesk.Revit.DB.XYZ object?",
                    Options = new() { "Millimeters", "Meters", "Decimal Feet", "Inches" },
                    CorrectOptionIndex = 2,
                    Explanation = "All geometric coordinates in Revit API XYZ objects are expressed in decimal feet."
                }
            }
        });

        // 57. Line and Vector Geometry
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-16",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 16,
            Title = "Line & Vector Geometry: Curves & Ray Math",
            Subtitle = "Line.CreateBound, DotProduct, CrossProduct, Normalize, and Curve endpoints",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Line inherits from Curve.",
                "Bound lines have defined start and end points (Line.CreateBound(pt1, pt2)).",
                "curve.GetEndPoint(0) is start; curve.GetEndPoint(1) is end.",
                "CrossProduct computes perpendicular normals; DotProduct computes angular alignments."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Creating Bounded Lines for Walls",
                    ContentHtml = "<p>System elements like Walls require a center line curve: <code>Line wallLine = Line.CreateBound(startPt, endPt);</code>.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Creating a Line and Computing Wall Normal",
                    Description = "Creating a line and calculating perpendicular normal vector.",
                    Code = @"XYZ p1 = new XYZ(0, 0, 0);
XYZ p2 = new XYZ(20, 0, 0);

// Create bounded curve:
Line line = Line.CreateBound(p1, p2);

// Tangent direction:
XYZ direction = line.Direction; // (1, 0, 0)

// Normal vector (cross product with Z-axis):
XYZ normal = direction.CrossProduct(XYZ.BasisZ).Normalize(); // (0, -1, 0)",
                    Explanation = "CrossProduct with BasisZ produces the horizontal face normal."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Creating Zero-Length Bound Lines",
                    Description = "Calling Line.CreateBound with two identical points throws an ArgumentException (line length is too short).",
                    BadCode = @"Line.CreateBound(new XYZ(0,0,0), new XYZ(0,0,0)); // CRASH!",
                    GoodCode = @"if (pt1.DistanceTo(pt2) > 1e-4) { Line.CreateBound(pt1, pt2); }",
                    WhyItMatters = "Revit enforces a minimum curve length of approximately 1/16th inch (0.0052 feet)."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What index represents the start point of a bounded Curve in curve.GetEndPoint(index)?",
                    Options = new() { "Index 0", "Index 1", "Index -1", "Index 2" },
                    CorrectOptionIndex = 0,
                    Explanation = "GetEndPoint(0) retrieves the start point, and GetEndPoint(1) retrieves the end point."
                }
            }
        });

        // 58. Element Geometry
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-17",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 17,
            Title = "Element Geometry: Options & GeometryElement",
            Subtitle = "elem.get_Geometry(Options), DetailLevel, ComputeReferences, and GeometryInstance",
            ReadTimeMinutes = 10,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Geometry is extracted via element.get_Geometry(Options).",
                "Options configures: DetailLevel (Coarse/Medium/Fine), ComputeReferences (for dimensioning), and View.",
                "GeometryElement contains GeometryObjects (Solids, Meshes, Curves, GeometryInstances).",
                "FamilyInstances nest their 3D geometry inside a GeometryInstance that must be unpacked."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Configuring Geometry Options",
                    ContentHtml = "<p>Always configure <code>Options</code> properly before extracting geometry:</p><pre class='p-2 bg-black rounded text-cyan font-monospace small'>Options opt = new Options {\n    DetailLevel = ViewDetailLevel.Fine,\n    ComputeReferences = true,\n    IncludeNonVisibleObjects = false\n};</pre>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Extracting Solids from Element Geometry",
                    Description = "Traversing GeometryElement to locate solid geometry.",
                    Code = @"Options opt = new Options { DetailLevel = ViewDetailLevel.Fine };
GeometryElement geomElem = wall.get_Geometry(opt);

if (geomElem != null)
{
    foreach (GeometryObject geomObj in geomElem)
    {
        if (geomObj is Solid solid && solid.Volume > 1e-6)
        {
            System.Console.WriteLine($""Found Solid with Volume: {solid.Volume:F2} cu.ft"");
        }
    }
}",
                    Explanation = "Filters out empty/void solids by checking solid.Volume > 1e-6."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Ignoring Empty Solids",
                    Description = "Revit often returns zero-volume helper solids with empty faces; attempting to read their faces throws exceptions.",
                    BadCode = @"foreach (var s in solids) { double a = s.Faces.Size; // Crashes on null/empty solids! }",
                    GoodCode = @"if (geomObj is Solid s && s.Volume > 1e-6) { /* Safe */ }",
                    WhyItMatters = "Boolean cuts leave zero-volume solid remnants in geometry trees."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What object must be passed to element.get_Geometry() to configure detail level and references?",
                    Options = new() { "Transaction", "Options", "FilteredElementCollector", "ViewSheet" },
                    CorrectOptionIndex = 1,
                    Explanation = "The Options class configures detail level, view context, and reference computation for geometry extraction."
                }
            }
        });

        // 59. Solids Utilities
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-18",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 18,
            Title = "Solids Utilities: Boolean Operations & Volume",
            Subtitle = "BooleanOperationsUtils (Union, Difference, Intersect) and solid volume calculation",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "BooleanOperationsUtils performs 3D CSG (Constructive Solid Geometry) operations on solids.",
                "ExecuteBooleanOperation(solidA, solidB, BooleanOperationsType.Union/Difference/Intersect).",
                "Enables clash detection, void cutouts, and volume intersection calculations in memory."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Calculating Clash Volume Between Ducts and Walls",
                    ContentHtml = "<p>By performing <code>BooleanOperationsType.Intersect</code> between a wall's solid and an MEP duct's solid, you can compute the exact 3D clash volume and penetration cross-section!</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Solid Intersection Calculation",
                    Description = "Computing the intersection of two solids.",
                    Code = @"// Intersect two solids in memory:
Solid? clashSolid = BooleanOperationsUtils.ExecuteBooleanOperation(
    wallSolid, 
    ductSolid, 
    BooleanOperationsType.Intersect);

if (clashSolid != null && clashSolid.Volume > 1e-6)
{
    System.Console.WriteLine($""Clash Detected! Intersection Volume: {clashSolid.Volume:F4} cu.ft"");
}",
                    Explanation = "Calculates exact geometric collision without altering the physical model."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Executing Booleans on Non-Intersecting Solids",
                    Description = "Attempting ExecuteBooleanOperation on disjoint solids can throw Autodesk.Revit.Exceptions.InvalidOperationException.",
                    BadCode = @"BooleanOperationsUtils.ExecuteBooleanOperation(s1, s2, BooleanOperationsType.Intersect);",
                    GoodCode = @"// First verify bounding boxes intersect before running heavy 3D solid CSG!",
                    WhyItMatters = "Bounding box pre-checking avoids expensive solid crash exceptions."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What utility class in Autodesk Revit API performs 3D solid boolean operations like Union and Intersect?",
                    Options = new() { "SolidUtils", "BooleanOperationsUtils", "GeometryUtils", "MeshCreator" },
                    CorrectOptionIndex = 1,
                    Explanation = "BooleanOperationsUtils provides ExecuteBooleanOperation for union, difference, and intersection."
                }
            }
        });

        // 60. Face and Edge Geometry
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-19",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 19,
            Title = "Face and Edge Geometry: PlanarFace & Normals",
            Subtitle = "PlanarFace, CylindricalFace, Edge loops, FaceNormal, and hosting dimensions",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Solids are bounded by Faces; Faces are bounded by Edges.",
                "PlanarFace has a constant FaceNormal vector (e.g. pointing up for floor top faces).",
                "Face.Reference allows creating automatic dimensions between parallel wall faces.",
                "CylindricalFace represents circular columns and pipes."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Finding the Exterior Face of a Wall",
                    ContentHtml = "<p>Iterate over <code>solid.Faces</code>: find the <code>PlanarFace</code> whose normal vector aligns with the wall's orientation vector (<code>wall.Orientation</code>).</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Extracting Top Face of a Floor/Slab",
                    Description = "Locating the planar face pointing directly upwards (+Z).",
                    Code = @"PlanarFace? topFace = null;

foreach (Face face in solid.Faces)
{
    if (face is PlanarFace pf)
    {
        // Check if face normal points straight up: (0, 0, 1)
        if (pf.FaceNormal.IsAlmostEqualTo(XYZ.BasisZ))
        {
            topFace = pf;
            break;
        }
    }
}

if (topFace != null)
{
    System.Console.WriteLine($""Top surface area: {topFace.Area:F2} sq.ft"");
}",
                    Explanation = "IsAlmostEqualTo(XYZ.BasisZ) cleanly identifies top horizontal surfaces."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Calling ComputeReferences = False When Dimensioning",
                    Description = "Attempting to create a Dimension using face.Reference when Options.ComputeReferences was false throws an exception.",
                    BadCode = @"Options opt = new Options(); // ComputeReferences is false by default!
// face.Reference is null! Dimension.Create crashes!",
                    GoodCode = @"Options opt = new Options { ComputeReferences = true };",
                    WhyItMatters = "Face and Edge references are only populated when explicitly requested."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What vector does PlanarFace.FaceNormal point towards for a horizontal floor top face?",
                    Options = new() { "XYZ.BasisX (1,0,0)", "XYZ.BasisY (0,1,0)", "XYZ.BasisZ (0,0,1)", "XYZ.Zero" },
                    CorrectOptionIndex = 2,
                    Explanation = "XYZ.BasisZ represents the positive vertical vector (0, 0, 1), pointing directly upwards."
                }
            }
        });

        // 61. Elements Creation
        mod.Topics.Add(new Topic
        {
            Id = "r-l1-20",
            ModuleId = "revit-lvl01",
            TrackId = "revitapi",
            Order = 20,
            Title = "Elements Creation: Walls, Floors & Instances",
            Subtitle = "Wall.Create, Floor.Create, doc.Create.NewFamilyInstance, and Levels",
            ReadTimeMinutes = 11,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Walls: Wall.Create(doc, curve, wallTypeId, levelId, height, offset, flip, isStructural).",
                "Floors: Floor.Create(doc, curveLoops, floorTypeId, levelId).",
                "Family Instances: doc.Create.NewFamilyInstance(point, symbol, level, structuralType).",
                "Always call symbol.Activate() and doc.Regenerate() before placing family instances!"
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The Complete Creation Pipeline",
                    ContentHtml = "<ol><li>Start a <code>Transaction</code>.</li><li>Ensure the type/symbol is active: <code>if (!symbol.IsActive) symbol.Activate();</code></li><li>Call the factory creation method (e.g. <code>Wall.Create</code>).</li><li>Call <code>doc.Regenerate()</code> if subsequent code depends on the new element's geometry.</li><li>Commit the transaction.</li></ol>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Creating a Wall and Inserting a Door",
                    Description = "Complete creation workflow in C#.",
                    Code = @"using (Transaction trans = new Transaction(doc, ""Create Wall & Door""))
{
    trans.Start();

    // 1. Create Wall along a 20-foot baseline
    Line line = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(20, 0, 0));
    Wall wall = Wall.Create(doc, line, wallType.Id, level.Id, 10.0, 0.0, false, false);
    doc.Regenerate(); // Regenerate so wall reference exists!

    // 2. Activate door symbol
    if (!doorSymbol.IsActive)
    {
        doorSymbol.Activate();
        doc.Regenerate();
    }

    // 3. Place Door hosted in Wall at mid-span
    XYZ location = new XYZ(10, 0, 0);
    FamilyInstance door = doc.Create.NewFamilyInstance(
        location, 
        doorSymbol, 
        wall, 
        level, 
        Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

    trans.Commit();
}",
                    Explanation = "The gold standard pattern for hosted element creation."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Missing doc.Regenerate() Between Dependent Creations",
                    Description = "Creating a hosted element (door/window) in a newly created wall without calling doc.Regenerate() in between fails because the wall geometry isn't resolved yet.",
                    BadCode = @"Wall.Create(...);
doc.Create.NewFamilyInstance(..., newWall, ...); // Error: Wall host reference not ready!",
                    GoodCode = @"Wall.Create(...);
doc.Regenerate(); // Resolves wall host in database
doc.Create.NewFamilyInstance(..., newWall, ...);",
                    WhyItMatters = "Revit needs regeneration to compute face and hosting references for new elements."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Why must you call doc.Regenerate() after creating a wall before inserting a hosted door into it in the same transaction?",
                    Options = new() { "To save the file to disk", "To compute the wall's geometric host references required by the door", "To change user permissions", "To refresh Windows Explorer" },
                    CorrectOptionIndex = 1,
                    Explanation = "Hosted elements require valid host references; doc.Regenerate() calculates the newly created wall's geometric references in memory."
                }
            }
        });

        return mod;
    }
}
