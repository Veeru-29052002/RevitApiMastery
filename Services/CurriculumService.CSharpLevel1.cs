using RevitApiMastery.Models;

namespace RevitApiMastery.Services;

public partial class CurriculumService
{
    private Module BuildCSharpLevel1Module()
    {
        var mod = new Module
        {
            Id = "c-lvl1",
            TrackId = "csharp",
            Order = 1,
            LevelNumber = 1,
            LevelTitle = "Stage 1: C# Foundations (Basics)",
            Title = "C# Stage 1: Foundations & Core Syntax",
            ShortDescription = "Complete fundamentals from Visual Studio setup and Addin Manager to data types, loops, collections, and LINQ.",
            Icon = "bi-terminal",
            Level = DifficultyLevel.Beginner
        };

        // 1. Introduction
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-01",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 1,
            Title = "Introduction to C# for BIM & Revit",
            Subtitle = "Why C# is the premier industry language for Autodesk Revit API automation",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "C# is a modern, statically typed, object-oriented language developed by Microsoft running on the .NET runtime.",
                "Autodesk Revit's internal database is written in C++ with native managed CLR wrappers exposed directly to C#.",
                "C# add-ins execute with maximum performance, full type safety, and direct IntelliSense support."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Why C# Over Python / Dynamo for Revit?",
                    ContentHtml = "<p>While Dynamo and Python (pyRevit) are great for rapid prototyping, production-grade enterprise tools require <strong>C#</strong>. C# gives you 10x-50x faster execution speed, compile-time error checking, native WPF integration, intellectual property protection (compiled DLLs), and seamless access to every single class in <code>RevitAPI.dll</code> and <code>RevitAPIUI.dll</code>.</p>",
                    CalloutText = "Every major commercial Revit add-in on the Autodesk App Store is authored in C#.",
                    CalloutType = "tip"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Hello Revit World C# Template",
                    Description = "The quintessential modern C# code structure.",
                    Code = @"// Welcome to Revit C# Automation
namespace BimAutomation;

public class Program
{
    public static void Main()
    {
        string message = ""Welcome to Revit API C# Master Academy!"";
        System.Console.WriteLine(message);
    }
}",
                    Explanation = "Demonstrates namespace declaration, class scoping, method signature, and standard output."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Targeting Wrong .NET Version",
                    Description = "Using an incompatible .NET version causes Revit to silently reject your add-in.",
                    BadCode = @"<TargetFramework>net9.0-windows</TargetFramework> <!-- Incompatible with Revit 2024 -->",
                    GoodCode = @"<TargetFramework>net48</TargetFramework> <!-- Revit 2021-2024 -->
<TargetFramework>net8.0-windows</TargetFramework> <!-- Revit 2025+ -->",
                    WhyItMatters = "Revit 2024 requires .NET Framework 4.8; Revit 2025 upgraded to .NET 8."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What runtime environment does C# compile to and execute within?",
                    Options = new() { "Java Virtual Machine (JVM)", "Common Language Runtime (CLR / .NET)", "Node.js V8 Engine", "Direct x86 Assembly without runtime" },
                    CorrectOptionIndex = 1,
                    Explanation = "C# compiles to Common Intermediate Language (CIL) executed by the Common Language Runtime (CLR)."
                }
            }
        });

        // 2. Visual Studio Installation
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-02",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 2,
            Title = "Visual Studio Installation",
            Subtitle = "Step-by-step setup of Visual Studio Community/Professional for Revit add-in development",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Visual Studio Community is 100% free for individual developers and small businesses.",
                "You MUST select the '.NET desktop development' workload during installation.",
                "Ensure both .NET Framework 4.8 SDK and .NET 8 SDK are checked in Individual Components."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Workload Selection Checklist",
                    ContentHtml = "<ol><li>Download Visual Studio Installer (2022 recommended).</li><li>Check <strong>.NET Desktop Development</strong> workload.</li><li>In the right-hand panel, ensure <strong>.NET Framework 4.8 targeting pack</strong> and <strong>.NET 8.0 SDK</strong> are selected.</li><li>Ensure <strong>Git for Windows</strong> is enabled for source control.</li></ol>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Verifying Installed .NET SDKs via CLI",
                    Description = "Run this in terminal to confirm your compiler toolchain.",
                    Code = @"# Open PowerShell or Command Prompt:
dotnet --list-sdks
dotnet --version",
                    Explanation = "Displays all installed .NET Core / .NET 8+ development SDKs."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Missing .NET Framework 4.8 Developer Pack",
                    Description = "Building add-ins for Revit 2020-2024 will error with 'The reference assemblies for .NETFramework,Version=v4.8 were not found'.",
                    BadCode = @"// Visual Studio Build Error MSB3644:
// The reference assemblies for .NETFramework,Version=v4.8 were not found.",
                    GoodCode = @"// Fix: Run Visual Studio Installer > Modify > Individual Components > Check '.NET Framework 4.8 targeting pack'",
                    WhyItMatters = "You cannot compile class libraries targeting Revit 2021-2024 without the 4.8 targeting pack."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Which workload must be selected in the Visual Studio Installer for Revit development?",
                    Options = new() { "ASP.NET and web development", ".NET desktop development", "Python development", "Mobile development with C++" },
                    CorrectOptionIndex = 1,
                    Explanation = ".NET desktop development provides the compilers, WPF designer, and class library templates needed for Revit add-ins."
                }
            }
        });

        // 3. VS Extensions
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-03",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 3,
            Title = "Essential VS Extensions for BIM Developers",
            Subtitle = "Supercharge your productivity with productivity extensions and Revit templates",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Extensions like 'Revit Add-in Template' automatically scaffold .addin manifests and reference paths.",
                "Roslyn Analyzers catch nullability, performance, and memory leaks before you ever press Run.",
                "GitHub Copilot / IntelliCode boosts boilerplate speed for repetitive Revit parameter lookups."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Recommended Visual Studio Extensions",
                    ContentHtml = "<ul><li><strong>Revit Add-in Template Wizard</strong>: Automatically generates Command and Application templates with exact GUIDs.</li><li><strong>Output Colorizer</strong>: Highlights Revit debug logs and stack traces in color.</li><li><strong>ILogger / Productivity Tools</strong>: Fast navigation across large multi-project CAD solutions.</li></ul>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Installing dotnet new Templates via CLI",
                    Description = "Installing official Revit add-in templates directly from NuGet.",
                    Code = @"# Install popular community Revit template:
dotnet new install Revit.Addin.Template

# Create a new solution from template:
dotnet new revit2025-addin -n BimModelAuditor",
                    Explanation = "Allows instantaneous scaffolding of zero-configuration Revit projects."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Installing Untrusted Extensions",
                    Description = "Certain outdated extensions can slow down Visual Studio IDE typing or interfere with build events.",
                    BadCode = @"// Visual Studio hangs on solution loading due to obsolete 3rd party VSIX plugins.",
                    GoodCode = @"// Keep extensions minimal: Microsoft official tools and verified community templates.",
                    WhyItMatters = "Maintains optimal IDE startup time and debugging responsiveness."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What is the primary benefit of a Revit Add-in Template extension?",
                    Options = new() { "It buys Revit licenses automatically", "It automatically scaffolds references, manifests (.addin), and boilerplate code", "It replaces the C# compiler with Python", "It renders 3D models inside Word" },
                    CorrectOptionIndex = 1,
                    Explanation = "Template extensions eliminate manual setup by generating correct assembly references and manifest files instantly."
                }
            }
        });

        // 4. Addin Manager Installation
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-04",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 4,
            Title = "Addin Manager Installation & Hot Reloading",
            Subtitle = "Eliminate the painful restart loop: modify code and re-run commands without restarting Revit",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Standard Revit development requires closing and restarting Revit every time you recompile your DLL.",
                "Autodesk Revit Add-In Manager loads your DLL dynamically in a temporary AppDomain, allowing live re-execution.",
                "Using Add-In Manager saves dozens of hours of developer waiting time per week."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The Re-compile Problem & Add-In Manager Solution",
                    ContentHtml = "<p>When Revit loads a <code>.dll</code>, Windows places a file lock on it. You cannot rebuild in Visual Studio while Revit is open! <strong>Revit Add-In Manager</strong> reads the assembly into memory or a shadow folder, so you can edit code, press Build in VS, and click 'Run' inside Revit immediately.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Post-Build Event for Addin Manager",
                    Description = "Automatically copying your DLL to a hot-reload location on every compile.",
                    Code = @"<!-- In your .csproj: -->
<Target Name=""PostBuild"" AfterTargets=""PostBuildEvent"">
  <Exec Command=""copy &quot;$(TargetPath)&quot; &quot;%APPDATA%\Autodesk\Revit\Addins\2025\HotReload\&quot; /Y"" />
</Target>",
                    Explanation = "Ensures the newest compiled assembly is always ready for Add-In Manager execution."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "File Lock Error CS2012 / MSB3021",
                    Description = "Attempting to build in Visual Studio while Revit is actively holding the DLL open.",
                    BadCode = @"// Visual Studio Error:
// Could not copy bin\Debug\MyTool.dll to target because it is being used by 'Revit.exe'",
                    GoodCode = @"// Solution: Use Add-In Manager (Manual Mode) or attach debugger with Hot Reload.",
                    WhyItMatters = "Prevents developer frustration from repeated 2-minute Revit reboot cycles."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Why is the Revit Add-In Manager an essential tool for developers?",
                    Options = new() { "It converts Revit files to DWG", "It allows developers to modify and re-execute add-in commands without restarting Revit", "It creates architectural families automatically", "It optimizes cloud rendering" },
                    CorrectOptionIndex = 1,
                    Explanation = "Add-In Manager bypasses Revit's DLL file lock, enabling rapid code-compile-test cycles without restarting Revit."
                }
            }
        });

        // 5. Data Types
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-05",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 5,
            Title = "C# Data Types: The Foundation of BIM Information",
            Subtitle = "Understanding Value Types vs Reference Types and Revit's StorageType alignment",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "C# is strictly type-safe: every variable and object has a declared, immutable type.",
                "Value Types (int, double, bool) store data directly in their memory location.",
                "Reference Types (string, classes, arrays) store a memory address pointer pointing to Heap data.",
                "Revit parameters strictly map to: Integer, Double, String, and ElementId."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Mapping C# Types to Revit StorageTypes",
                    ContentHtml = "<table class='table table-dark table-sm'><thead><tr><th>C# Type</th><th>Revit StorageType</th><th>Typical BIM Example</th></tr></thead><tbody><tr><td><code>int</code></td><td>StorageType.Integer</td><td>Phase, Function (Interior/Exterior), True/False</td></tr><tr><td><code>double</code></td><td>StorageType.Double</td><td>Length (Feet), Area (Sq.Ft), Volume, Angle</td></tr><tr><td><code>string</code></td><td>StorageType.String</td><td>Comments, Mark, Manufacturer, Model</td></tr><tr><td><code>ElementId</code></td><td>StorageType.ElementId</td><td>LevelId, MaterialId, HostId</td></tr></tbody></table>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "C# Primitive Declarations for BIM",
                    Description = "Declaring explicit and inferred data types.",
                    Code = @"// Explicit type declaration
int wallCount = 42;
double wallThicknessFeet = 0.82; // Decimal feet
bool isLoadBearing = true;
char structuralUsageKey = 'S';
string panelMark = ""W-104-N"";

// Inferred type declaration using 'var' (strongly typed at compile time)
var calculationTimestamp = System.DateTime.UtcNow;",
                    Explanation = "'var' is NOT dynamic like JavaScript; it is strictly resolved by the C# compiler at compile time."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Confusing 'var' with JavaScript 'var'",
                    Description = "Assuming 'var' in C# allows changing types at runtime.",
                    BadCode = @"var count = 10; // C# infers 'int'
count = ""ten""; // COMPILE ERROR: Cannot assign string to int!",
                    GoodCode = @"var count = 10;
string countText = ""ten""; // Dedicated types",
                    WhyItMatters = "C# remains 100% statically typed even when using the 'var' keyword."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What are the 4 fundamental StorageTypes used by Revit element parameters?",
                    Options = new() { "Float, Char, Object, Array", "Integer, Double, String, ElementId", "Boolean, Date, Text, Number", "Decimal, Long, XML, JSON" },
                    CorrectOptionIndex = 1,
                    Explanation = "Autodesk Revit stores all parameter data internally as either Integer, Double, String, or ElementId."
                }
            }
        });

        // 6. Variables
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-06",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 6,
            Title = "Variables: Declaration, Initialization & Scope",
            Subtitle = "Best practices for naming, scoping, and lifetime in Revit command execution",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Variables must be declared and initialized before they can be read.",
                "Scope determines where a variable is accessible (block scope, method scope, class scope).",
                "Follow standard C# conventions: camelCase for local variables, PascalCase for properties and methods."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Variable Lifetime in Revit Commands",
                    ContentHtml = "<p>Variables declared inside your <code>Execute()</code> method exist only while the command is running. Once the command returns <code>Result.Succeeded</code>, local variables go out of scope and their memory is queued for Garbage Collection.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Scope & Lifetime Demonstration",
                    Description = "Block scoping inside a transaction block.",
                    Code = @"public void AuditModel()
{
    int totalElements = 0; // Method scope: accessible throughout AuditModel

    {
        int batchCount = 10; // Block scope: accessible ONLY inside these braces
        totalElements += batchCount;
    }

    // batchCount is destroyed here and cannot be accessed
    System.Console.WriteLine($""Total: {totalElements}"");
}",
                    Explanation = "Keeps variables tightly scoped to reduce cognitive load and prevent accidental side effects."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Leaking Static Variables Across Commands",
                    Description = "Storing document-specific elements in 'static' variables causes memory leaks and stale references.",
                    BadCode = @"public class MyCommand {
    public static List<int> CachedElementIds = new(); // DANGEROUS: Stays alive forever!
}",
                    GoodCode = @"public class MyCommand {
    // Keep data local to the method:
    public void Process() {
        var localIds = new List<int>();
    }
}",
                    WhyItMatters = "Static variables remain in RAM even after closing or switching documents, corrupting future executions."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What happens to a local variable declared inside a method when the method finishes executing?",
                    Options = new() { "It is saved to the Revit .rvt file automatically", "It goes out of scope and becomes eligible for Garbage Collection", "It becomes a static global variable", "It causes a compiler warning" },
                    CorrectOptionIndex = 1,
                    Explanation = "Local variables are destroyed when their enclosing block exits, freeing their stack frame."
                }
            }
        });

        // 7. INT Type
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-07",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 7,
            Title = "INT Type (System.Int32)",
            Subtitle = "32-bit signed integers in BIM: counts, element IDs, and bitwise flags",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "An 'int' is a 32-bit signed integer ranging from -2,147,483,648 to 2,147,483,647.",
                "Revit Element IDs historically map to 32-bit integers (elem.Id.IntegerValue in <=2023, GetIdValue() in 2024+).",
                "Revit Yes/No parameters are stored as integers (1 = Yes/True, 0 = No/False)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Revit Yes/No Parameters are Integers!",
                    ContentHtml = "<p>In the Revit user interface, parameters like <em>'Structural'</em> or <em>'Room Bounding'</em> look like checkboxes. In the database API, their <code>StorageType</code> is <strong>Integer</strong>, where <code>1</code> indicates checked (True) and <code>0</code> indicates unchecked (False).</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Working with Integer Values in Revit",
                    Description = "Setting and reading integer parameters.",
                    Code = @"// Setting a Yes/No parameter (stored as 0 or 1)
int isStructural = 1; // 1 = True
param.Set(isStructural);

// Reading an integer parameter:
int functionType = param.AsInteger();
if (functionType == 1) // 1 = Exterior in WALL_ATTR_FUNCTION
{
    System.Console.WriteLine(""Exterior Wall"");
}",
                    Explanation = "Demonstrates how integer parameters govern Revit element metadata."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Integer Division Truncation",
                    Description = "Dividing two integers in C# drops the decimal fraction completely.",
                    BadCode = @"int wallLength = 7;
int segmentCount = 2;
double segmentLength = wallLength / segmentCount; // Results in 3.0, NOT 3.5!",
                    GoodCode = @"double segmentLength = (double)wallLength / segmentCount; // Results in 3.5",
                    WhyItMatters = "Truncating fractions in dimension math causes geometry placement errors."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "How does Revit store a 'Yes/No' checkbox parameter in its internal database?",
                    Options = new() { "As a string 'true' / 'false'", "As an integer where 1 is Yes and 0 is No", "As an XML boolean node", "As a double 1.0 / 0.0" },
                    CorrectOptionIndex = 1,
                    Explanation = "Revit Yes/No parameters are StorageType.Integer with 1 representing Yes and 0 representing No."
                }
            }
        });

        // 8. Double Type
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-08",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 8,
            Title = "Double Type (System.Double)",
            Subtitle = "64-bit IEEE floating point math: the heartbeat of 3D BIM geometry",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "A 'double' is a 64-bit IEEE 754 floating-point number providing 15-17 digits of precision.",
                "All Revit geometric coordinates (XYZ) and dimension parameters (Length, Area, Volume) are doubles.",
                "Revit length values are ALWAYS in decimal feet, never millimeters or meters."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Floating Point Precision in CAD",
                    ContentHtml = "<p>Due to binary representation limits, operations like <code>0.1 + 0.2</code> yield <code>0.30000000000000004</code>. In 3D geometry algorithms, you must never compare doubles using <code>==</code>. Always use a geometric tolerance (epsilon), typically <code>1e-6</code> or <code>1e-9</code>.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Comparing Doubles with Epsilon Tolerance",
                    Description = "Safe geometric equality comparison.",
                    Code = @"public static class GeometryUtils
{
    public const double PrecisionTolerance = 1e-6; // 0.000001 feet

    public static bool IsEqual(double a, double b, double tolerance = PrecisionTolerance)
    {
        return System.Math.Abs(a - b) < tolerance;
    }
}",
                    Explanation = "Standard method used by all professional CAD geometry kernels."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Direct Double Equality Check",
                    Description = "Using == to test coordinates fails due to microscopic rounding errors.",
                    BadCode = @"if (point1.X == point2.X) { /* Fails 40% of the time */ }",
                    GoodCode = @"if (Math.Abs(point1.X - point2.X) < 1e-6) { /* Reliable comparison */ }",
                    WhyItMatters = "Prevents false negatives when checking if two wall endpoints join."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What is the recommended tolerance (epsilon) when comparing Revit geometric double coordinates?",
                    Options = new() { "10.0 feet", "1.0 foot", "1e-6 (0.000001) or smaller", "0.5 meters" },
                    CorrectOptionIndex = 2,
                    Explanation = "A tolerance of 1e-6 feet (or Revit's internal tolerance) accurately absorbs floating-point calculation inaccuracies."
                }
            }
        });

        // 9. Boolean Type
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-09",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 9,
            Title = "Boolean Type (bool)",
            Subtitle = "True/False logic driving validation, permissions, and conditional execution",
            ReadTimeMinutes = 5,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "A 'bool' holds either 'true' or 'false'.",
                "Used extensively for element validation: elem.IsValidObject, symbol.IsActive, trans.HasStarted().",
                "Revit API methods frequently return bool to indicate operation success."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Critical Revit Booleans",
                    ContentHtml = "<ul><li><code>elem.IsValidObject</code>: Checks if an element is still valid or was deleted.</li><li><code>doc.IsModified</code>: Checks if the project has unsaved changes.</li><li><code>symbol.IsActive</code>: Checks if a family type is loaded into memory ready for placement.</li></ul>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Defensive Boolean Validation",
                    Description = "Checking object validity before performing modifications.",
                    Code = @"if (!familySymbol.IsActive)
{
    familySymbol.Activate();
    doc.Regenerate();
}

bool canPlace = familySymbol.IsValidObject && familySymbol.IsActive;
if (canPlace)
{
    // Proceed with family placement
}",
                    Explanation = "Guarantees that the symbol is ready before instantiating elements."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Accessing Deleted Elements",
                    Description = "Accessing properties on an element that was deleted in an earlier transaction.",
                    BadCode = @"// If element was deleted:
string name = deletedElem.Name; // Throws InvalidObjectException!",
                    GoodCode = @"if (elem != null && elem.IsValidObject)
{
    string name = elem.Name;
}",
                    WhyItMatters = "Calling methods on deleted elements crashes the add-in."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Which property checks whether an Autodesk Revit Element is still valid in the database?",
                    Options = new() { "elem.IsAlive", "elem.IsValidObject", "elem.Exists", "elem.IsPresent" },
                    CorrectOptionIndex = 1,
                    Explanation = "elem.IsValidObject returns false if the native C++ pointer to the element has been deleted or invalidated."
                }
            }
        });

        // 10. Char Type
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-10",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 10,
            Title = "Char Type (System.Char)",
            Subtitle = "Single 16-bit Unicode characters for grid lines, parsing delimiters, and codes",
            ReadTimeMinutes = 5,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "A 'char' represents a single UTF-16 code unit enclosed in single quotes ('A', '1', '\\t').",
                "Useful for grid column labels (Grid 'A', Grid 'B') and parsing CSV delimiters (',', ';').",
                "char.IsDigit, char.IsLetter, and char.ToUpper provide fast character analysis."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Single Quotes vs Double Quotes",
                    ContentHtml = "<p>In C#, single quotes <code>'A'</code> declare a <strong>char</strong> (value type, 2 bytes). Double quotes <code>\"A\"</code> declare a <strong>string</strong> (reference type, heap allocated object).</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Auto-Incrementing Grid Names with Char",
                    Description = "Generating alphabetic grid bubble labels.",
                    Code = @"char currentGridLetter = 'A';

for (int i = 0; i < 5; i++)
{
    string gridName = currentGridLetter.ToString();
    System.Console.WriteLine($""Created Grid Line: {gridName}"");
    currentGridLetter++; // Advances 'A' -> 'B' -> 'C'...
}",
                    Explanation = "In C#, adding an integer to a char advances to the next Unicode character code."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Using Double Quotes for Char Literal",
                    Description = "Writing \"A\" instead of 'A' when assigning to a char variable.",
                    BadCode = @"char delimiter = "";""; // COMPILE ERROR: Cannot implicitly convert string to char",
                    GoodCode = @"char delimiter = ';'; // Correct: single quotes for char",
                    WhyItMatters = "Strings and chars are fundamentally distinct types in C#."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What syntax declares a 'char' literal in C#?",
                    Options = new() { "Double quotes: \"C\"", "Single quotes: 'C'", "Backticks: `C`", "Brackets: [C]" },
                    CorrectOptionIndex = 1,
                    Explanation = "Char literals in C# must be enclosed in single quotes ('X')."
                }
            }
        });

        // 11. String Type
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-11",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 11,
            Title = "String Type (System.String)",
            Subtitle = "Immutable text, string interpolation ($), StringBuilder, and parameter parsing",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Strings in C# are reference types, but are IMMUTABLE (cannot be changed once created).",
                "String interpolation ($'Value: {x}') provides readable formatting.",
                "Modifying strings in a tight loop across 10,000 elements causes severe GC memory pressure; use StringBuilder.",
                "Always check string.IsNullOrWhiteSpace(str) before reading parameter values."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "String Immutability & Memory Impact",
                    ContentHtml = "<p>Every time you concatenate <code>str = str + newText</code> inside a loop over 20,000 elements, a brand new string object is allocated on the managed heap, discarding the old one. Use <code>System.Text.StringBuilder</code> for high-speed batch text processing.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "String Interpolation & StringBuilder",
                    Description = "Building an element report cleanly and efficiently.",
                    Code = @"// String Interpolation
string mark = ""D-101"";
double width = 3.0;
string description = $""Door Mark: {mark}, Clear Width: {width:F2} ft"";

// StringBuilder for High-Speed Batch Generation
var sb = new System.Text.StringBuilder(capacity: 10000);
foreach (var wall in walls)
{
    sb.AppendLine($""ID: {wall.Id} | Category: {wall.Category.Name}"");
}
string finalReport = sb.ToString();",
                    Explanation = "StringBuilder modifies an internal buffer without creating garbage objects."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "NullReferenceException on AsString()",
                    Description = "Calling .AsString() on a parameter that is empty or unset returns null.",
                    BadCode = @"string val = elem.LookupParameter(""Comments"").AsString();
if (val.Contains(""Audit"")) // CRASH if Comments is blank/null!",
                    GoodCode = @"string val = elem.LookupParameter(""Comments"")?.AsString() ?? string.Empty;
if (val.Contains(""Audit"", StringComparison.OrdinalIgnoreCase)) { /* Safe */ }",
                    WhyItMatters = "Revit returns null for unset parameters, causing crashes on unhandled dereferences."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What class should be used to concatenate text inside a loop processing 50,000 BIM elements?",
                    Options = new() { "string (using + operator)", "System.Text.StringBuilder", "List<char>", "System.IO.MemoryStream" },
                    CorrectOptionIndex = 1,
                    Explanation = "StringBuilder mutates an internal buffer without creating thousands of short-lived heap strings, avoiding GC pauses."
                }
            }
        });

        // 12. Operators
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-12",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 12,
            Title = "Operators: Arithmetic & Math Calculations",
            Subtitle = "Addition, subtraction, multiplication, division, modulo, and geometric formulas",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Arithmetic operators (+, -, *, /, %) perform numeric computations.",
                "Modulo operator (%) calculates remainders, useful for batch chunking or grid patterns.",
                "System.Math provides Sqrt, Sin, Cos, Atan2, and PI for 3D vector transformations."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Converting Degrees to Radians",
                    ContentHtml = "<p>Revit internal rotation APIs (like <code>ElementTransformUtils.RotateElement</code>) ALWAYS take rotation angles in <strong>Radians</strong>, not degrees!</p><p>Formula: <code>radians = degrees * (Math.PI / 180.0)</code>.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Geometric Math Formula in C#",
                    Description = "Calculating distance and angle between two points.",
                    Code = @"double x1 = 0, y1 = 0;
double x2 = 10, y2 = 10;

// Euclidean 2D distance: sqrt(dx^2 + dy^2)
double dx = x2 - x1;
double dy = y2 - y1;
double distance = System.Math.Sqrt(dx * dx + dy * dy);

// Angle in radians and degrees:
double angleRadians = System.Math.Atan2(dy, dx);
double angleDegrees = angleRadians * (180.0 / System.Math.PI);",
                    Explanation = "Essential math for rotating elements along wall curves."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Passing Degrees to RotateElement()",
                    Description = "Passing 90 to RotateElement rotates the element 90 RADIANS (approx 5,156 degrees), scrambling model geometry!",
                    BadCode = @"// DANGEROUS: Passes 90 radians!
ElementTransformUtils.RotateElement(doc, elemId, axis, 90.0);",
                    GoodCode = @"// Correct: Convert degrees to radians
double radians = 90.0 * (Math.PI / 180.0);
ElementTransformUtils.RotateElement(doc, elemId, axis, radians);",
                    WhyItMatters = "Revit API angular parameters strictly require radians."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "In what angular unit does ElementTransformUtils.RotateElement expect its angle parameter?",
                    Options = new() { "Degrees", "Radians", "Gradians", "Percentage of slope" },
                    CorrectOptionIndex = 1,
                    Explanation = "All angular rotations in the Autodesk Revit API must be supplied in Radians."
                }
            }
        });

        // 13. Assignments
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-13",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 13,
            Title = "Assignment Operators & Compound Assignments",
            Subtitle = "=, +=, -=, *=, /=, and null-coalescing assignment (??=)",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Compound assignments (+=, -=) modify and reassign a variable in place.",
                "Null-coalescing assignment (??=) assigns a value ONLY if the variable is currently null.",
                "Essential for tallying quantities and initializing lazy collections."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Modern Null-Coalescing Assignment (??=)",
                    ContentHtml = "<p>Instead of writing <code>if (cache == null) cache = new List&lt;Element&gt;();</code>, modern C# provides <code>cache ??= new List&lt;Element&gt;();</code>.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Accumulator and Lazy Initialization",
                    Description = "Using += and ??= in a BIM parameter tallying routine.",
                    Code = @"double totalWallArea = 0.0;
totalWallArea += 120.5; // Compound addition
totalWallArea += 85.2;

// Lazy collection initialization:
List<string>? warningsList = null;
warningsList ??= new List<string>(); // Initializes only because it was null!
warningsList.Add(""Warning: Unhosted door detected"");",
                    Explanation = "Reduces boilerplate code and ensures thread-safe initialization."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Overwriting Parameter Values by Mistake",
                    Description = "Assigning a new value using '=' instead of appending with '+='.",
                    BadCode = @"string comments = elem.LookupParameter(""Comments"").AsString();
comments = ""Audited""; // Discards previous comments!",
                    GoodCode = @"string existing = elem.LookupParameter(""Comments"")?.AsString() ?? """";
string updated = string.IsNullOrEmpty(existing) ? ""Audited"" : $""{existing}; Audited"";",
                    WhyItMatters = "Accidentally wipes existing design notes and mark tracking."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What does the '??=' operator do in modern C#?",
                    Options = new() { "Checks if two numbers are equal", "Assigns the right-hand value only if the left-hand variable is null", "Performs integer division", "Encrypts a string" },
                    CorrectOptionIndex = 1,
                    Explanation = "The null-coalescing assignment operator '??=' assigns the right operand to the left operand only if the left operand evaluates to null."
                }
            }
        });

        // 14. Comparisons
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-14",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 14,
            Title = "Comparison Operators",
            Subtitle = "==, !=, <, >, <=, >= and relational checks across BIM elements",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Comparison operators return a boolean (true or false).",
                "ElementId equality: id1 == id2 checks if both represent the same database element.",
                "Always be mindful of reference equality vs value equality."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "ElementId Equality in Revit",
                    ContentHtml = "<p>In Autodesk Revit API, <code>ElementId</code> implements <code>==</code> and <code>!=</code> operators. Comparing <code>id1 == id2</code> compares their underlying integer IDs, not their C# memory addresses.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Comparing Element Categories and Heights",
                    Description = "Relational logic for architectural clearance validation.",
                    Code = @"double sillHeight = window.LookupParameter(""Sill Height"")?.AsDouble() ?? 0.0;
const double MinClearanceFeet = 2.95; // ~900mm

if (sillHeight < MinClearanceFeet)
{
    System.Console.WriteLine($""Safety Warning: Window {window.Id} sill height is below safety threshold!"");
}",
                    Explanation = "Uses '<' to flag design compliance violations."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Comparing Element Proxies with ==",
                    Description = "Comparing two distinct managed Element instances representing the same database entity using object reference equality.",
                    BadCode = @"// If elemA and elemB are distinct wrapper objects:
if (elemA == elemB) { /* Might be false even if they represent the same wall! */ }",
                    GoodCode = @"// Compare their ElementId:
if (elemA.Id == elemB.Id) { /* 100% reliable */ }",
                    WhyItMatters = "Revit may instantiate multiple C# wrapper objects for the same underlying C++ entity."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What is the most reliable way to check if two C# Element variables represent the same Revit element?",
                    Options = new() { "elem1 == elem2", "elem1.Id == elem2.Id", "elem1.Name == elem2.Name", "elem1.ToString() == elem2.ToString()" },
                    CorrectOptionIndex = 1,
                    Explanation = "Comparing elem1.Id == elem2.Id compares their unique integer database IDs, which is guaranteed to be accurate."
                }
            }
        });

        // 15. Logicals
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-15",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 15,
            Title = "Logical Operators: AND, OR, NOT",
            Subtitle = "&&, ||, ! and short-circuit evaluation in complex BIM filter rules",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Logical AND (&&) requires both conditions to be true.",
                "Logical OR (||) requires at least one condition to be true.",
                "Logical NOT (!) inverts boolean state.",
                "Short-circuiting: in (a && b), if 'a' is false, 'b' is NEVER evaluated (critical for null checks)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The Power of Short-Circuit Evaluation",
                    ContentHtml = "<p>In <code>if (param != null &amp;&amp; param.HasValue)</code>, if <code>param</code> is null, C# short-circuits and skips <code>param.HasValue</code>, preventing a <code>NullReferenceException</code> crash!</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Safe Short-Circuit Null Checking",
                    Description = "Validating parameter presence and value safely.",
                    Code = @"Parameter? commentsParam = wall.LookupParameter(""Comments"");

// Short-circuit: param.AsString() is NEVER called if commentsParam is null
if (commentsParam != null && commentsParam.HasValue && commentsParam.AsString() == ""Phase 1"")
{
    System.Console.WriteLine(""Wall belongs to Phase 1"");
}",
                    Explanation = "Protects against null pointer dereferences without deeply nested if statements."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Using Single & Instead of Double &&",
                    Description = "Using '&' disables short-circuiting and evaluates both sides even if the first is null.",
                    BadCode = @"// DANGEROUS: Single & evaluates right side even if left is null!
if (param != null & param.HasValue) { /* Crashes if param is null! */ }",
                    GoodCode = @"if (param != null && param.HasValue) { /* Safe short-circuit */ }",
                    WhyItMatters = "Single '&' is a bitwise operator and does not guard against null references."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What happens in 'if (obj != null && obj.IsValid)' when 'obj' is null?",
                    Options = new() { "It crashes with NullReferenceException", "It skips evaluating obj.IsValid because && short-circuits", "It converts obj to an integer", "It displays an alert dialog" },
                    CorrectOptionIndex = 1,
                    Explanation = "The && operator short-circuits: since the left condition is false, the right condition is never evaluated."
                }
            }
        });

        // 16. Data Types Conversion
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-16",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 16,
            Title = "Data Types Conversion & Parsing",
            Subtitle = "Implicit/explicit casting, int.Parse, double.TryParse, and Convert",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Implicit casting happens automatically when no precision is lost (int -> double).",
                "Explicit casting requires a cast operator (double -> int: (int)val).",
                "Never use double.Parse() on user input; always use double.TryParse() to prevent crashes on invalid strings."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Cultural Invariance in Number Parsing",
                    ContentHtml = "<p>In English locales, numbers use a period <code>\"3.5\"</code>. In European locales (French, German), numbers use a comma <code>\"3,5\"</code>! Always pass <code>CultureInfo.InvariantCulture</code> to avoid unexpected parse errors.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Bulletproof Culture-Invariant Parsing",
                    Description = "Safely parsing a string into a geometric coordinate.",
                    Code = @"using System.Globalization;

string userInput = ""12.5"";

// Safe Parsing with InvariantCulture
if (double.TryParse(userInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsedValue))
{
    System.Console.WriteLine($""Parsed coordinate: {parsedValue} ft"");
}
else
{
    System.Console.WriteLine(""Invalid numeric input entered."");
}",
                    Explanation = "TryParse returns false cleanly instead of throwing a FormatException."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Using double.Parse() without TryParse",
                    Description = "Calling double.Parse on a textbox string throws FormatException when the user types a letter or comma.",
                    BadCode = @"double offset = double.Parse(txtOffset.Text); // CRASH on bad input!",
                    GoodCode = @"if (double.TryParse(txtOffset.Text, out double offset)) { /* safe */ }",
                    WhyItMatters = "Unhandled parsing exceptions crash the Revit session."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What method should you use to convert a string from a user input textbox into a double?",
                    Options = new() { "double.Parse()", "(double)string", "double.TryParse()", "Convert.ToDoubleUnsafe()" },
                    CorrectOptionIndex = 2,
                    Explanation = "double.TryParse safely parses strings and returns false if the text is invalid, avoiding unhandled exceptions."
                }
            }
        });

        // 17. Comments
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-17",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 17,
            Title = "Comments & XML Documentation",
            Subtitle = "// Single-line, /* Multi-line */, and /// XML Doc comments for APIs",
            ReadTimeMinutes = 5,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Comments document the 'why', not the obvious 'what'.",
                "Triple-slash comments (///) generate XML documentation for IntelliSense tooltips across your team.",
                "Well-commented Revit commands significantly reduce onboarding time for new developers."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "XML Documentation Comments",
                    ContentHtml = "<p>Typing <code>///</code> above any method in Visual Studio automatically generates <code>&lt;summary&gt;</code>, <code>&lt;param&gt;</code>, and <code>&lt;returns&gt;</code> tags. When other developers call your method, Visual Studio displays these notes in hover tooltips.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Professional XML Documentation Template",
                    Description = "Documenting a Revit geometric utility method.",
                    Code = @"/// <summary>
/// Calculates the total volume of all walls matching the specified fire rating.
/// </summary>
/// <param name=""doc"">The active Autodesk Revit Document.</param>
/// <param name=""minRatingHours"">Minimum fire safety rating in hours.</param>
/// <returns>Total volume in cubic feet.</returns>
public double CalculateTotalFireWallVolume(Document doc, double minRatingHours)
{
    // Implementation here...
    return 0.0;
}",
                    Explanation = "Feeds directly into Visual Studio's IntelliSense engine."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Outdated Comments",
                    Description = "Updating code logic without updating existing comments leads to false assumptions.",
                    BadCode = @"// Divides length by 2 (Actual code divides by 4):
double segment = length / 4.0;",
                    GoodCode = @"// Divides into 4 equal quarters for panel distribution:
double segment = length / 4.0;",
                    WhyItMatters = "Misleading comments waste hours during debugging."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "How do you trigger XML documentation comments in Visual Studio C#?",
                    Options = new() { "Type //", "Type /*", "Type /// directly above a class or method", "Type --" },
                    CorrectOptionIndex = 2,
                    Explanation = "Typing '///' above a code member automatically generates structured XML documentation tags."
                }
            }
        });

        // 18. Regions
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-18",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 18,
            Title = "#region & #endregion Directives",
            Subtitle = "Structuring large classes, collapsing boilerplate, and code layout",
            ReadTimeMinutes = 5,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "#region and #endregion are preprocessor directives for organizing code blocks in the IDE.",
                "Enables collapsing sections in Visual Studio (e.g., #region Revit Command Logic).",
                "Do not use #region to hide sprawling, unmaintainable 1,000-line methods; refactor instead."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Standard Class Layout with Regions",
                    ContentHtml = "<ol><li><code>#region Fields and Constants</code></li><li><code>#region Properties</code></li><li><code>#region Constructors</code></li><li><code>#region Public Methods</code></li><li><code>#region Private Helper Methods</code></li></ol>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Organizing a Command Class with #region",
                    Description = "Collapsible logical sections in Visual Studio.",
                    Code = @"public class WallAuditCommand : IExternalCommand
{
    #region IExternalCommand Implementation
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        // Primary command execution
        return Result.Succeeded;
    }
    #endregion

    #region Helper Methods
    private double ConvertFeetToMeters(double feet) => feet * 0.3048;
    #endregion
}",
                    Explanation = "Keeps large files tidy and easy to navigate."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Using Regions to Hide Spaghetti Code",
                    Description = "Wrapping hundreds of poorly architected lines in a region called 'Do Not Open' instead of breaking into clean classes.",
                    BadCode = @"#region Giant Mess - 800 lines of mixed code",
                    GoodCode = @"// Split into dedicated services: WallFilterService, VolumeCalculator, ExcelExporter",
                    WhyItMatters = "Regions should structure clean code, not conceal technical debt."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What is the purpose of #region in C#?",
                    Options = new() { "It defines a new thread", "It specifies a collapsible code block in code editors without affecting compilation", "It allocates unmanaged memory", "It imports an external DLL" },
                    CorrectOptionIndex = 1,
                    Explanation = "#region is a compiler directive used by IDEs to collapse and organize sections of code for readability."
                }
            }
        });

        // 19. Lists
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-19",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 19,
            Title = "Lists (List<T>): The Dynamic BIM Collection",
            Subtitle = "Adding, removing, indexing, sorting, and manipulating generic lists of elements",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "List<T> is a strongly typed, dynamically resizing list backed by an internal array.",
                "Provides fast index-based lookup: list[0].",
                "Methods: .Add(), .AddRange(), .Remove(), .Clear(), .Contains(), .Count.",
                "The primary collection returned when materializing Revit queries (collector.ToElements().ToList())."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Pre-allocating List Capacity",
                    ContentHtml = "<p>When you know you are fetching 10,000 elements from Revit, instantiate <code>new List&lt;ElementId&gt;(capacity: 10000)</code>. This avoids repeated array resizing and memory reallocation as items are added.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Working with List<ElementId>",
                    Description = "Creating and populating an ElementId collection.",
                    Code = @"// Initialize list with initial capacity
var selectedIds = new List<ElementId>(capacity: 500);

selectedIds.Add(new ElementId(1001));
selectedIds.Add(new ElementId(1002));

// Batch check and count
if (selectedIds.Count > 0)
{
    System.Console.WriteLine($""Total IDs queued: {selectedIds.Count}"");
}",
                    Explanation = "Demonstrates efficient generic list usage."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Modifying a List Inside a Foreach Loop",
                    Description = "Calling list.Remove() inside a foreach loop over that same list throws InvalidOperationException: Collection was modified.",
                    BadCode = @"foreach (var elem in wallList) {
    if (elem.Id.IntegerValue < 0) wallList.Remove(elem); // CRASH!
}",
                    GoodCode = @"// Option 1: Loop backwards with for loop
// Option 2: Use RemoveAll
wallList.RemoveAll(elem => elem.Id.IntegerValue < 0);",
                    WhyItMatters = "Foreach uses an internal enumerator that invalidates if the collection size changes."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What happens if you call list.Remove(item) inside a foreach loop enumerating that list?",
                    Options = new() { "It removes the item cleanly", "It throws an InvalidOperationException (Collection was modified)", "It duplicates the item", "The loop restarts from index 0" },
                    CorrectOptionIndex = 1,
                    Explanation = "Modifying a collection during foreach enumeration invalidates the enumerator and throws an InvalidOperationException."
                }
            }
        });

        // 20. Foreach Loop
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-20",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 20,
            Title = "Foreach Loop: Iterating Over BIM Models",
            Subtitle = "The cleanest, most idiomatic way to iterate over IEnumerable collections and collectors",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Foreach operates on any type implementing IEnumerable or IEnumerable<T>.",
                "Revit's FilteredElementCollector implements IEnumerable, allowing direct foreach iteration.",
                "Iteration variable is read-only (you cannot reassign the loop pointer)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Direct Iteration on FilteredElementCollector",
                    ContentHtml = "<p>You don't even need to call <code>.ToElements()</code> to iterate! You can directly <code>foreach (Element elem in collector)</code>. This streams elements one at a time, conserving memory.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Direct Foreach on Revit Collector",
                    Description = "Streaming elements directly from native database memory.",
                    Code = @"FilteredElementCollector collector = new FilteredElementCollector(doc)
    .OfCategory(BuiltInCategory.OST_Walls)
    .WhereElementIsNotElementType();

// Streams elements one by one without buffering an entire list in RAM!
foreach (Element wall in collector)
{
    string wallName = wall.Name;
    System.Console.WriteLine($""Processing wall: {wallName}"");
}",
                    Explanation = "Highly memory-efficient pattern for large models."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Unnecessary Materialization with .ToElements()",
                    Description = "Calling collector.ToElements().ToList() when you only need to iterate once allocates thousands of unused list slots.",
                    BadCode = @"foreach (var elem in collector.ToElements().ToList()) { /* duplicate memory allocation */ }",
                    GoodCode = @"foreach (var elem in collector) { /* Direct streaming enumeration */ }",
                    WhyItMatters = "Direct enumeration reduces Garbage Collector pressure in huge enterprise models."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What interface must a collection implement in C# to support 'foreach' loops?",
                    Options = new() { "ICloneable", "IEnumerable", "IDisposable", "IComparable" },
                    CorrectOptionIndex = 1,
                    Explanation = "The foreach statement iterates over any object that implements IEnumerable or exposes a GetEnumerator() method."
                }
            }
        });

        // 21. For Loop
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-21",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 21,
            Title = "For Loop: Index-Based Iteration",
            Subtitle = "Index control, backward iteration, and safely removing elements while looping",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "For loop syntax: for (int i = 0; i < count; i++).",
                "Allows index-based access: list[i].",
                "Reverse iteration (for int i = count - 1; i >= 0; i--) safely allows removing items while looping.",
                "Faster than foreach for raw arrays where index operations are required."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Safe Deletion via Reverse For Loop",
                    ContentHtml = "<p>When filtering a list of elements to delete or prune, looping backwards from <code>list.Count - 1</code> down to <code>0</code> ensures that removing index <code>i</code> does not change the indices of unvisited items!</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Reverse For Loop for Safe Removal",
                    Description = "Removing invalid elements safely.",
                    Code = @"List<ElementId> idList = GetCandidateElementIds();

// Loop backwards:
for (int i = idList.Count - 1; i >= 0; i--)
{
    if (idList[i].IntegerValue < 0)
    {
        idList.RemoveAt(i); // Safe! Doesn't shift unvisited items
    }
}",
                    Explanation = "The standard algorithm for in-place list pruning."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Off-By-One Errors (<= count)",
                    Description = "Writing i <= list.Count throws IndexOutOfRangeException on the final step.",
                    BadCode = @"for (int i = 0; i <= list.Count; i++) // Throws exception at i == list.Count!",
                    GoodCode = @"for (int i = 0; i < list.Count; i++) // Correct",
                    WhyItMatters = "Indices are 0-based; the last element is at list.Count - 1."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What index is the last element in a C# list with 10 elements?",
                    Options = new() { "Index 10", "Index 9", "Index 1", "Index 0" },
                    CorrectOptionIndex = 1,
                    Explanation = "C# collections are zero-indexed, meaning a list of length 10 has elements at indices 0 through 9."
                }
            }
        });

        // 22. If Statement
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-22",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 22,
            Title = "If Statement: Conditional Branching",
            Subtitle = "if, else if, else branching logic for model validation and rule checking",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "If statements execute a code block only if their condition evaluates to true.",
                "Use 'else if' for sequential mutual-exclusion checks.",
                "Always place curly braces {} around if blocks to prevent accidental indentation bugs."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Clean Guard Clauses",
                    ContentHtml = "<p>Instead of deeply nesting <code>if (a) { if (b) { if (c) { ... } } }</code>, use <strong>Guard Clauses</strong>: check for negative conditions and return early.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Guard Clause Pattern",
                    Description = "Flattening nested if statements for maximum readability.",
                    Code = @"public Result ExecuteCommand(Document doc, Element elem)
{
    // Guard clause 1:
    if (doc == null || elem == null)
        return Result.Failed;

    // Guard clause 2:
    if (!elem.IsValidObject)
        return Result.Cancelled;

    // Primary business logic runs cleanly at indentation level 0!
    ProcessElement(elem);
    return Result.Succeeded;
}",
                    Explanation = "Eliminates the 'arrowhead anti-pattern' of deep nesting."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Accidental Assignment Inside If (a = b)",
                    Description = "Writing single '=' instead of '==' (though C# type checker prevents this for non-bools, for bools it causes subtle bugs).",
                    BadCode = @"bool isVerified = false;
if (isVerified = true) // Assigns true, condition always passes!",
                    GoodCode = @"if (isVerified) // Idiomatic boolean check",
                    WhyItMatters = "Leads to conditional branches executing unconditionally."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What is the primary benefit of using 'guard clauses' in a Revit API command?",
                    Options = new() { "It makes the command run multi-threaded", "It validates preconditions early and flattens nested indentation", "It deletes temporary files", "It opens AutoCAD" },
                    CorrectOptionIndex = 1,
                    Explanation = "Guard clauses exit early if preconditions fail, keeping the main code clean and un-nested."
                }
            }
        });

        // 23. If Statement Shorthand
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-23",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 23,
            Title = "If Statement Shorthand: The Ternary Operator",
            Subtitle = "condition ? trueExpression : falseExpression",
            ReadTimeMinutes = 5,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "The ternary operator returns one of two values based on a boolean expression.",
                "Syntax: condition ? valueIfTrue : valueIfFalse.",
                "Concise, inline, and perfect for UI formatting and status labels."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Ternary Operator in BIM Reports",
                    ContentHtml = "<p>Formatting reports: <code>string status = isCompliant ? \"PASSED\" : \"FAILED\";</code> is much cleaner than 5 lines of <code>if/else</code> assignment.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Inline Ternary Formatting",
                    Description = "Formatting element instance state inline.",
                    Code = @"bool isLoadBearing = wall.get_Parameter(BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT)?.AsInteger() == 1;

string wallTypeLabel = isLoadBearing ? ""Structural Shear Wall"" : ""Architectural Partition"";
System.Console.WriteLine($""Wall Type: {wallTypeLabel}"");",
                    Explanation = "Evaluates and assigns the label in a single line."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Deeply Nested Ternary Operators",
                    Description = "Nesting multiple ternaries (a ? b : c ? d : e ? f : g) makes code unreadable.",
                    BadCode = @"string desc = a ? (b ? ""X"" : ""Y"") : (c ? ""Z"" : ""W"");",
                    GoodCode = @"// Use a switch expression or clear if/else block instead!",
                    WhyItMatters = "Nested ternaries are notoriously difficult to read and maintain."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What operator represents the inline ternary conditional in C#?",
                    Options = new() { "??", "? :", "::", "=>" },
                    CorrectOptionIndex = 1,
                    Explanation = "The ternary operator is written as '? :' (e.g. condition ? valueIfTrue : valueIfFalse)."
                }
            }
        });

        // 24. Break and Continue
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-24",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 24,
            Title = "Break and Continue: Loop Flow Control",
            Subtitle = "Halting execution early with 'break' and skipping iterations with 'continue'",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "'break' immediately terminates the innermost enclosing loop.",
                "'continue' skips the rest of the current iteration and jumps to the next loop step.",
                "Crucial when searching through 50,000 elements for a specific match."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Early Exit Optimization",
                    ContentHtml = "<p>If you are searching for a specific FamilySymbol by name, once you find it, call <code>break;</code> immediately! Don't continue looping over the remaining 9,999 elements.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Break and Continue in BIM Filtering",
                    Description = "Skipping unhosted elements and halting upon match.",
                    Code = @"FamilyInstance? targetDoor = null;

foreach (FamilyInstance door in doorList)
{
    // Skip if door has no host wall
    if (door.Host == null)
        continue;

    // Check for target mark
    if (door.LookupParameter(""Mark"")?.AsString() == ""D-TARGET"")
    {
        targetDoor = door;
        break; // Found match! Stop scanning remaining items
    }
}",
                    Explanation = "Combines 'continue' for skipping invalid items and 'break' for stopping upon completion."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Using Break in Nested Loops",
                    Description = "'break' only exits the innermost loop, not outer enclosing loops.",
                    BadCode = @"foreach (var level in levels) {
    foreach (var wall in walls) {
        if (found) break; // Only exits the wall loop, NOT level loop!
    }
}",
                    GoodCode = @"// Return directly or use a boolean flag / LINQ FirstOrDefault",
                    WhyItMatters = "Outer loops continue to execute, wasting CPU cycles."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What statement immediately skips the rest of the current loop cycle and advances to the next item?",
                    Options = new() { "break", "continue", "return", "goto" },
                    CorrectOptionIndex = 1,
                    Explanation = "The 'continue' statement immediately jumps to the next iteration of the enclosing loop."
                }
            }
        });

        // 25. Switch Statement
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-25",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 25,
            Title = "Switch Statements & Pattern Matching",
            Subtitle = "switch/case, break, and modern C# pattern-matching switch expressions",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Switch statements route execution based on an expression value.",
                "Modern C# switch expressions return values directly with concise syntax.",
                "Essential for routing Revit BuiltInCategory enums and user command actions."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Switch Expressions with BuiltInCategories",
                    ContentHtml = "<p>Instead of 10 nested <code>if/else</code> statements checking categories, modern C# lets you pattern match cleanly.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Modern Switch Expression on BuiltInCategory",
                    Description = "Mapping categories to discipline groupings.",
                    Code = @"public string GetDiscipline(BuiltInCategory category) => category switch
{
    BuiltInCategory.OST_Walls or 
    BuiltInCategory.OST_Floors or 
    BuiltInCategory.OST_Roofs => ""Architecture"",

    BuiltInCategory.OST_StructuralColumns or 
    BuiltInCategory.OST_StructuralFraming => ""Structure"",

    BuiltInCategory.OST_DuctCurves or 
    BuiltInCategory.OST_PipeCurves => ""MEP"",

    _ => ""General"" // Discard pattern (default)
};",
                    Explanation = "Concise, compiler-verified pattern matching with zero boilerplate."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Forgetting Break in Traditional Switch",
                    Description = "In traditional switch statements, omitting 'break' causes compiler error CS0163: Control cannot fall through.",
                    BadCode = @"switch (categoryName) {
    case ""Walls"": DoWork(); // Error: missing break!
}",
                    GoodCode = @"switch (categoryName) {
    case ""Walls"": DoWork(); break;
}",
                    WhyItMatters = "C# forbids implicit fall-through between non-empty switch cases."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What symbol represents the 'default' case in a modern C# switch expression?",
                    Options = new() { "default", "*", "_ (underscore / discard)", "else" },
                    CorrectOptionIndex = 2,
                    Explanation = "The underscore '_' is the discard pattern in C# switch expressions, acting as the catch-all default branch."
                }
            }
        });

        // 26. While Loop
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-26",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 26,
            Title = "While Loop: Condition-Controlled Repetition",
            Subtitle = "Looping while a condition remains true: queue draining, linked lists, and iterative geometry",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "While loops evaluate their condition BEFORE executing the loop body.",
                "Ideal for draining queues (e.g. processing connected MEP duct networks).",
                "Always ensure the loop condition eventually becomes false to prevent infinite freezes."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Draining a Processing Queue with While",
                    ContentHtml = "<p>When traversing MEP networks (pipes or ducts connected via Connectors), a <code>Queue&lt;Element&gt;</code> with <code>while (queue.Count &gt; 0)</code> is the canonical traversal pattern.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Network Traversal Using While Loop",
                    Description = "Draining a Queue of connected elements.",
                    Code = @"Queue<ElementId> elementsToVisit = new Queue<ElementId>();
HashSet<ElementId> visitedIds = new HashSet<ElementId>();

elementsToVisit.Enqueue(startingDuctId);

while (elementsToVisit.Count > 0)
{
    ElementId currentId = elementsToVisit.Dequeue();
    visitedIds.Add(currentId);

    // Find and enqueue neighboring connected elements...
}",
                    Explanation = "Standard Breadth-First-Search (BFS) network traversal."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Infinite While Loops Freezing Revit UI",
                    Description = "A while loop where the condition never becomes false causes Revit to hang indefinitely, forcing a task manager kill.",
                    BadCode = @"while (elementsRemaining > 0) {
    // If an error occurs and elementsRemaining is not decremented, infinite loop!
}",
                    GoodCode = @"int maxSafetyIterations = 10000;
while (elementsRemaining > 0 && --maxSafetyIterations > 0) {
    // Guaranteed to terminate!
}",
                    WhyItMatters = "Revit runs on a single UI thread; infinite loops freeze the entire CAD session."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What happens if a while loop condition never becomes false?",
                    Options = new() { "The loop stops after 100 cycles automatically", "The program enters an infinite loop, freezing the UI thread", "It throws a TimeoutException", "It restarts Windows" },
                    CorrectOptionIndex = 1,
                    Explanation = "Without an exit condition, a while loop runs infinitely, locking the CPU and freezing the host application."
                }
            }
        });

        // 27. Do While Loop
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-27",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 27,
            Title = "Do While Loop: Guaranteed Single Execution",
            Subtitle = "Executing at least once before evaluating the condition: retry logic & prompts",
            ReadTimeMinutes = 5,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "Do-while loops evaluate the condition AFTER the loop body executes.",
                "Guarantees that the code block runs at least once.",
                "Ideal for user retry prompts and multi-attempt transactions."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Retry Patterns with Do-While",
                    ContentHtml = "<p>When reading cloud BIM data or acquiring file locks, a do-while loop allows attempting the action first, and retrying up to a maximum attempt limit if a transient failure occurs.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Transient Retry with Do-While",
                    Description = "Attempting a file read with retries.",
                    Code = @"int attempts = 0;
bool success = false;

do
{
    attempts++;
    success = TryConnectToLicenseServer();
    if (!success) System.Threading.Thread.Sleep(500); // Wait 500ms before retry
}
while (!success && attempts < 3);

if (!success)
{
    System.Console.WriteLine(""Failed to acquire license after 3 attempts."");
}",
                    Explanation = "Executes the initial connection attempt before evaluating loop conditions."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Missing Semicolon After While",
                    Description = "Forgetting the mandatory semicolon at the end of 'do { ... } while (condition);'.",
                    BadCode = @"do { ... } while (condition) // Missing semicolon causes compiler error!",
                    GoodCode = @"do { ... } while (condition); // Correct",
                    WhyItMatters = "Do-while is one of the few loop statements in C# requiring a trailing semicolon."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "How many times is a 'do-while' loop guaranteed to execute at minimum?",
                    Options = new() { "0 times", "At least 1 time", "At least 2 times", "Unlimited times" },
                    CorrectOptionIndex = 1,
                    Explanation = "Because the condition is evaluated at the bottom of the loop, a do-while loop always executes at least once."
                }
            }
        });

        // 28. LINQ
        mod.Topics.Add(new Topic
        {
            Id = "c-l1-28",
            ModuleId = "c-lvl1",
            TrackId = "csharp",
            Order = 28,
            Title = "LINQ (Language Integrated Query)",
            Subtitle = "The supreme data processing engine: Where, Select, OrderBy, GroupBy, and FirstOrDefault",
            ReadTimeMinutes = 11,
            Level = DifficultyLevel.Beginner,
            KeyTakeaways = new()
            {
                "LINQ transforms raw collections into expressive, declarative data queries.",
                "Method syntax (.Where().Select()) is the industry standard for Revit developers.",
                "Deferred execution: queries only execute when enumerated (foreach, ToList, Count).",
                "Always apply Revit native QuickFilters before executing .NET LINQ expressions."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Essential LINQ Methods for BIM Developers",
                    ContentHtml = "<ul><li><code>.Where(x => ...):</code> Filters elements matching a predicate.</li><li><code>.Select(x => ...):</code> Projects/transforms elements into new shapes (e.g., getting all ElementIds).</li><li><code>.FirstOrDefault(x => ...):</code> Finds first match or returns null safely.</li><li><code>.GroupBy(x => ...):</code> Groups elements (e.g., walls grouped by Level).</li><li><code>.OrderBy(x => ...):</code> Sorts elements (e.g., doors sorted by Mark).</li></ul>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Professional LINQ on Revit Elements",
                    Description = "Sorting and grouping elements with LINQ method syntax.",
                    Code = @"// Filter, project, group, and order in one fluent chain:
var wallsByLevel = wallElements
    .Where(w => w.LookupParameter(""Comments"")?.AsString() != ""Ignore"")
    .GroupBy(w => w.LevelId)
    .Select(g => new
    {
        LevelId = g.Key,
        WallCount = g.Count(),
        TotalLength = g.Sum(w => w.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH)?.AsDouble() ?? 0.0)
    })
    .OrderByDescending(x => x.TotalLength)
    .ToList();",
                    Explanation = "High-speed declarative aggregation of model quantities."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Using .First() Instead of .FirstOrDefault()",
                    Description = "Calling .First() throws InvalidOperationException: Sequence contains no matching element when no items match.",
                    BadCode = @"// CRASHES if no door matches:
var targetDoor = doors.First(d => d.Name == ""Special"");",
                    GoodCode = @"// Returns null cleanly:
var targetDoor = doors.FirstOrDefault(d => d.Name == ""Special"");
if (targetDoor != null) { /* Safe */ }",
                    WhyItMatters = "Prevents crashes when expected model elements do not exist."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What LINQ method safely returns null if no matching element is found, instead of throwing an exception?",
                    Options = new() { "First()", "FirstOrDefault()", "Single()", "FindOrThrow()" },
                    CorrectOptionIndex = 1,
                    Explanation = "FirstOrDefault() returns default(T) (null for reference types) if the sequence is empty or no match is found."
                }
            }
        });

        return mod;
    }
}
