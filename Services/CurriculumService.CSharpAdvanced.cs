using RevitApiMastery.Models;

namespace RevitApiMastery.Services;

public partial class CurriculumService
{
    private Module BuildCSharpAdvancedModule()
    {
        var mod = new Module
        {
            Id = "c-adv",
            TrackId = "csharp",
            Order = 2,
            LevelNumber = 2,
            LevelTitle = "Stage 2: C# ADVANCED (OOP & Architecture)",
            Title = "C# Stage 2: Object-Oriented Architecture",
            ShortDescription = "Classes, encapsulation, inheritance, polymorphism, interfaces, and bulletproof exception handling.",
            Icon = "bi-diagram-3",
            Level = DifficultyLevel.Intermediate
        };

        // 29. Namespaces
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-01",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 1,
            Title = "Namespaces & Scoping",
            Subtitle = "Organizing assemblies, preventing naming collisions, and file-scoped namespaces",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Namespaces group related classes, interfaces, and enums into logical domains.",
                "Prevents type collisions (e.g. Autodesk.Revit.DB.View vs System.Windows.Forms.View).",
                "C# 10 file-scoped namespaces (namespace MyProject;) eliminate unnecessary indentation."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Resolving Revit Naming Collisions with Aliases",
                    ContentHtml = "<p>A frequent Revit API dilemma is that both <code>Autodesk.Revit.DB</code> and <code>System.Windows.Forms</code> contain a class named <code>View</code>. You can resolve this using a namespace alias:</p><p><code>using RevitView = Autodesk.Revit.DB.View;</code></p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Modern File-Scoped Namespaces with Aliases",
                    Description = "Clean C# file header in a Revit add-in.",
                    Code = @"// C# 10 File-Scoped Namespace (No extra curly braces!)
namespace BimEnterprise.Auditing;

// Type Aliases to avoid collisions between Revit API and System libraries
using RevitView = Autodesk.Revit.DB.View;
using RevitElement = Autodesk.Revit.DB.Element;

public class ViewAuditService
{
    public void InspectView(RevitView view)
    {
        System.Console.WriteLine($""Inspecting active view: {view.Name}"");
    }
}",
                    Explanation = "Prevents ambiguity without typing full namespace paths repeatedly."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Ambiguous Reference Compiler Error CS0104",
                    Description = "Importing both Autodesk.Revit.DB and System.Windows.Controls causes 'View' or 'Point' to become ambiguous.",
                    BadCode = @"using Autodesk.Revit.DB;
using System.Windows;
// Point pt; -> ERROR: 'Point' is an ambiguous reference!",
                    GoodCode = @"using Autodesk.Revit.DB;
using Point = System.Windows.Point; // Explicit alias resolves conflict",
                    WhyItMatters = "Ambiguous references break compilation."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "How do you resolve an ambiguous class name collision between Revit and .NET libraries?",
                    Options = new() { "Delete one of the DLLs", "Create a type alias using 'using Alias = FullNamespace.Type;'", "Rename the Revit API", "Downgrade C#" },
                    CorrectOptionIndex = 1,
                    Explanation = "Using a type alias (e.g. using RevitView = Autodesk.Revit.DB.View;) disambiguates colliding names cleanly."
                }
            }
        });

        // 30. Class and Object
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-02",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 2,
            Title = "Class and Object: Blueprints & Instances",
            Subtitle = "Modeling real-world architectural components as C# reference types",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "A Class is a blueprint; an Object is an instantiated instance living in heap memory.",
                "In Revit, FamilySymbol is like a Class, and FamilyInstance is like an Object.",
                "Classes bundle state (fields, properties) with behavior (methods)."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Revit Analogy: Symbol vs Instance",
                    ContentHtml = "<p>Think of a C# <code>Class</code> like a Revit <code>FamilySymbol</code> (e.g. \"Single-Flush: 36x84\"). It defines the dimensions and materials. An <code>Object</code> is like a placed <code>FamilyInstance</code> in the floor plan (e.g. Door #104 on Level 1).</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Defining a Custom BIM Data Transfer Object",
                    Description = "Class definition representing an audited wall.",
                    Code = @"public class AuditedWall
{
    public int ElementId { get; set; }
    public string WallType { get; set; } = string.Empty;
    public double LengthFeet { get; set; }
    public bool HasFireRating { get; set; }

    public double GetLengthInMeters() => LengthFeet * 0.3048;
}

// Instantiating objects:
var wall1 = new AuditedWall
{
    ElementId = 5021,
    WallType = ""Exterior - Brick on CMU"",
    LengthFeet = 32.5,
    HasFireRating = true
};",
                    Explanation = "Encapsulates wall audit data cleanly with object initializer syntax."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Treating Classes as Value Types",
                    Description = "Assigning one class variable to another copies the reference pointer, NOT the object data.",
                    BadCode = @"AuditedWall a = new AuditedWall { LengthFeet = 10 };
AuditedWall b = a; // Both 'a' and 'b' point to the SAME object!
b.LengthFeet = 20; // 'a.LengthFeet' is now also 20!",
                    GoodCode = @"// To duplicate, create a new instance or use modern record with-expressions.",
                    WhyItMatters = "Accidental shared mutation creates difficult-to-trace bugs across views."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What is the relationship between a Class and an Object in C#?",
                    Options = new() { "A class is compiled code; an object is source code", "A class is a blueprint; an object is an instantiated memory instance of that blueprint", "They are identical in every way", "Classes are for databases; objects are for UI" },
                    CorrectOptionIndex = 1,
                    Explanation = "A class defines properties and methods; creating an instance with 'new' allocates an object on the heap."
                }
            }
        });

        // 31. Fields
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-03",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 3,
            Title = "Fields: Internal State Storage",
            Subtitle = "Private backing fields, readonly fields, and encapsulation",
            ReadTimeMinutes = 6,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Fields are variables declared directly inside a class body to store internal state.",
                "Fields should almost always be marked 'private' (Encapsulation).",
                "The 'readonly' keyword ensures a field can only be assigned during declaration or inside the constructor."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The Power of 'readonly' in Revit Add-ins",
                    ContentHtml = "<p>When your command passes the <code>Document</code> or <code>UIApplication</code> into helper services, store them in <code>private readonly Document _doc;</code>. This prevents bugs where the document pointer is accidentally overwritten midway through a calculation.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Readonly Document Field Pattern",
                    Description = "Service class maintaining immutable document reference.",
                    Code = @"public class RoomAuditService
{
    // Private readonly field: immutable after constructor
    private readonly Document _document;
    private readonly double _minAreaThreshold;

    public RoomAuditService(Document document, double minAreaThreshold = 50.0)
    {
        _document = document ?? throw new ArgumentNullException(nameof(document));
        _minAreaThreshold = minAreaThreshold;
    }
}",
                    Explanation = "The leading underscore (_document) is the standard C# convention for private fields."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Public Mutable Fields",
                    Description = "Exposing public fields breaks encapsulation and allows external code to corrupt internal state.",
                    BadCode = @"public class WallProcessor {
    public int ProcessedCount; // Anyone can change this from the outside!
}",
                    GoodCode = @"public class WallProcessor {
    public int ProcessedCount { get; private set; } // Read-only from outside
}",
                    WhyItMatters = "Encapsulation ensures the class maintains full control over how its state transitions."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "When can a 'readonly' field be assigned a value in C#?",
                    Options = new() { "At any time anywhere", "Only at declaration or within class constructors", "Only inside a static method", "Only in the destructor" },
                    CorrectOptionIndex = 1,
                    Explanation = "Readonly fields can only be set at their declaration or inside constructors of the enclosing class."
                }
            }
        });

        // 32. Access Modifiers
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-04",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 4,
            Title = "Access Modifiers: public, private, protected, internal",
            Subtitle = "Controlling visibility, API surface area, and assembly boundaries",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "public: Accessible anywhere.",
                "private: Accessible only within the enclosing class (default for class members).",
                "internal: Accessible anywhere within the same compiled assembly (.dll).",
                "protected: Accessible within the class and derived child classes."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Why IExternalCommand MUST be Public",
                    ContentHtml = "<p>When Autodesk Revit inspects your add-in DLL via Reflection, it searches for <strong>public</strong> classes implementing <code>IExternalCommand</code>. If your class is marked <code>internal</code> or <code>private</code>, Revit cannot instantiate it, and your tool will fail to load!</p>",
                    CalloutText = "Always ensure your Command and Application classes have 'public' visibility.",
                    CalloutType = "warning"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Access Modifier Visibility Demo",
                    Description = "Layered accessibility in a Revit plugin.",
                    Code = @"// Must be public for Revit Reflection loader:
public class ExportWallsCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var helper = new InternalBimHelper();
        helper.DoPrivateWork();
        return Result.Succeeded;
    }
}

// Internal: hidden from consumers of your DLL, visible across your add-in
internal class InternalBimHelper
{
    internal void DoPrivateWork() => Step1();
    private void Step1() { /* strictly private helper */ }
}",
                    Explanation = "Hides internal implementation details while exposing only the public contract."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Internal Command Class",
                    Description = "Omitting the 'public' keyword on an IExternalCommand class makes it internal by default.",
                    BadCode = @"class MyCommand : IExternalCommand { /* Revit cannot load this! */ }",
                    GoodCode = @"public class MyCommand : IExternalCommand { /* Loaded successfully */ }",
                    WhyItMatters = "Revit logs: 'Could not create an instance of the external command type'."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What access modifier must be applied to an IExternalCommand class so Revit can load it?",
                    Options = new() { "private", "protected", "internal", "public" },
                    CorrectOptionIndex = 3,
                    Explanation = "Revit Reflection requires the command class to be 'public' to instantiate it across assembly boundaries."
                }
            }
        });

        // 33. Constructor
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-05",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 5,
            Title = "Constructors: Object Initialization",
            Subtitle = "Constructors, constructor overloading, 'this()' chaining, and primary constructors",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "A constructor has the exact same name as the class and no return type.",
                "Executes automatically when 'new' is called.",
                "Used to inject dependencies (like Document or UI services) and validate required state.",
                "C# 12+ supports Primary Constructors directly on the class declaration line."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Constructor Overloading and Chaining",
                    ContentHtml = "<p>Use <code>this(...)</code> to chain multiple constructors, avoiding duplicate setup logic.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Constructor Chaining & Primary Constructor",
                    Description = "Traditional constructor chaining vs modern C# 12 primary constructors.",
                    Code = @"// Traditional Constructor Overloading:
public class WallFilterOptions
{
    public double MinLength { get; }
    public bool IncludeCurtainWalls { get; }

    // Default constructor: chains to parameterized constructor
    public WallFilterOptions() : this(minLength: 1.0, includeCurtainWalls: false) { }

    public WallFilterOptions(double minLength, bool includeCurtainWalls)
    {
        MinLength = minLength;
        IncludeCurtainWalls = includeCurtainWalls;
    }
}

// Modern C# 12 Primary Constructor:
public class LevelService(Document doc, string defaultLevelName)
{
    public Document Document => doc;
}",
                    Explanation = "Primary constructors drastically reduce boilerplate in modern C#."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Adding Parameters to IExternalCommand Constructor",
                    Description = "Revit requires a parameterless constructor to instantiate your command.",
                    BadCode = @"public class MyCommand : IExternalCommand {
    public MyCommand(string param) { /* Revit crashes: No parameterless constructor! */ }
}",
                    GoodCode = @"public class MyCommand : IExternalCommand {
    public MyCommand() { /* Default constructor for Revit */ }
}",
                    WhyItMatters = "Activator.CreateInstance() fails if no parameterless constructor exists."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What kind of constructor must an IExternalCommand class expose for Autodesk Revit to load it?",
                    Options = new() { "A constructor taking Document as an argument", "A public parameterless constructor", "A private constructor", "A static constructor only" },
                    CorrectOptionIndex = 1,
                    Explanation = "Revit's add-in host creates commands via Activator.CreateInstance, requiring a public parameterless constructor."
                }
            }
        });

        // 34. Properties
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-06",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 6,
            Title = "Properties: Controlled Encapsulation",
            Subtitle = "Auto-properties, expression-bodied properties, and init-only setters",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Properties look like fields to the caller, but execute get and set accessor code.",
                "Auto-implemented properties (public int Id { get; set; }) automatically generate hidden backing fields.",
                "Init-only properties (get; init;) allow assignment ONLY during object initialization.",
                "Essential for WPF XAML data binding in Revit UI add-ins."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Expression-Bodied Computed Properties",
                    ContentHtml = "<p>Instead of writing a full getter, compute derived values inline: <code>public double AreaM2 =&gt; AreaSqFt * 0.092903;</code></p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Modern Property Patterns",
                    Description = "Immutable properties, validation setters, and computed getters.",
                    Code = @"public class RoomSummary
{
    // Auto-property with init-only (Immutable after construction)
    public string RoomNumber { get; init; } = string.Empty;

    // Backing field with validation
    private double _areaSqFt;
    public double AreaSqFt
    {
        get => _areaSqFt;
        set => _areaSqFt = value >= 0 ? value : throw new ArgumentException(""Area cannot be negative"");
    }

    // Expression-bodied computed property (Revit Feet to Metric Sq.Meters):
    public double AreaSqMeters => AreaSqFt * 0.092903;
}",
                    Explanation = "Provides clean data exposure while maintaining validation invariants."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Infinite Recursion in Property Setters",
                    Description = "Accidentally referencing the property itself instead of the backing field inside its setter.",
                    BadCode = @"public string Name {
    get => Name; // Infinite recursion! StackOverflowException!
    set => Name = value;
}",
                    GoodCode = @"private string _name;
public string Name {
    get => _name;
    set => _name = value;
}",
                    WhyItMatters = "Causes immediate StackOverflowException crashes."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What does the 'init' accessor on a property indicate?",
                    Options = new() { "It can only be read, never assigned", "It can be assigned during object construction, but becomes immutable afterwards", "It runs multi-threaded", "It deletes initial values" },
                    CorrectOptionIndex = 1,
                    Explanation = "Init-only properties allow assignment during object initialization (via constructor or object initializers), then remain read-only."
                }
            }
        });

        // 35. Methods
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-07",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 7,
            Title = "Methods: Behavior & Return Values",
            Subtitle = "Parameters, return types, expression-bodied methods, and clean single-responsibility",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Methods encapsulate a sequence of statements performing a distinct action.",
                "Follow the Single Responsibility Principle (SRP): one method should do one thing well.",
                "Use expression-bodied syntax (=>) for concise one-line transformations."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Refactoring Long Revit Commands into Methods",
                    ContentHtml = "<p>Instead of 400 continuous lines in <code>Execute()</code>, extract methods: <code>CollectWalls()</code>, <code>ValidateParameters()</code>, <code>CommitUpdates()</code>.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Clean Method Decomposition",
                    Description = "Extracting focused methods for Revit operations.",
                    Code = @"public class WallAuditManager
{
    // Clean top-level orchestration method
    public void ExecuteAudit(Document doc)
    {
        var walls = GetEligibleWalls(doc);
        ProcessWallParameters(walls);
    }

    // Focused single-responsibility query:
    private IList<Wall> GetEligibleWalls(Document doc) =>
        new FilteredElementCollector(doc)
            .OfClass(typeof(Wall))
            .WhereElementIsNotElementType()
            .Cast<Wall>()
            .ToList();

    // Focused mutation method:
    private void ProcessWallParameters(IEnumerable<Wall> walls)
    {
        foreach (var wall in walls)
        {
            wall.LookupParameter(""Comments"")?.Set(""Audited"");
        }
    }
}",
                    Explanation = "Highly readable, testable, and maintainable structure."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "God Methods with 20 Parameters",
                    Description = "Creating methods with 15-20 individual arguments instead of bundling parameters into an options object.",
                    BadCode = @"public void CreateWall(Document doc, double x1, double y1, double z1, double x2, double y2, double z2, int typeId, int levelId, double h, double off, bool flip, bool structural) { ... }",
                    GoodCode = @"public void CreateWall(Document doc, WallCreationOptions options) { ... }",
                    WhyItMatters = "Options objects prevent parameter-order mixups and simplify maintenance."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What software design principle states that a method should have one clear purpose and reason to change?",
                    Options = new() { "Open-Closed Principle", "Single Responsibility Principle (SRP)", "Liskov Substitution Principle", "Interface Segregation" },
                    CorrectOptionIndex = 1,
                    Explanation = "The Single Responsibility Principle dictates that a method should be focused on performing a single distinct job."
                }
            }
        });

        // 36. Methods Overloading
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-08",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 8,
            Title = "Method Overloading: Multiple Signatures",
            Subtitle = "Same method name with distinct parameter counts, types, or orders",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Method overloading allows declaring multiple methods with the exact same name but different signatures.",
                "The compiler selects the correct overload based on argument types at compile time.",
                "Revit API uses overloading extensively (e.g. FilteredElementCollector(doc) vs FilteredElementCollector(doc, viewId))."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Revit API Overloads in Action",
                    ContentHtml = "<p>Notice how Revit's collector has two major overloads:</p><ul><li><code>new FilteredElementCollector(doc)</code> &rarr; Searches entire project database.</li><li><code>new FilteredElementCollector(doc, view.Id)</code> &rarr; Scopes search to visible elements in that view.</li></ul>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Designing Overloaded Utility Methods",
                    Description = "Overloading element finder by ID or by Name.",
                    Code = @"public class ElementFinder
{
    private readonly Document _doc;
    public ElementFinder(Document doc) => _doc = doc;

    // Overload 1: Find by integer ID
    public Element? Find(int elementId) => _doc.GetElement(new ElementId(elementId));

    // Overload 2: Find by ElementId object
    public Element? Find(ElementId elementId) => _doc.GetElement(elementId);

    // Overload 3: Find by name string
    public Element? Find(string elementName) =>
        new FilteredElementCollector(_doc)
            .WhereElementIsNotElementType()
            .FirstOrDefault(e => e.Name == elementName);
}",
                    Explanation = "Provides convenient calling conventions without forcing conversion."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Overloading Solely by Return Type",
                    Description = "C# does not allow two methods with identical parameter signatures that differ only by return type.",
                    BadCode = @"int GetLength();
double GetLength(); // COMPILE ERROR: Cannot overload based on return type alone",
                    GoodCode = @"int GetLengthInt();
double GetLengthDouble(); // Distinct names or parameter types",
                    WhyItMatters = "Method signature consists of the name and parameter types, NOT return type."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Can two overloaded methods in C# differ ONLY by their return type?",
                    Options = new() { "Yes, always", "No, C# does not allow overloading based solely on return type", "Only if marked static", "Only inside interfaces" },
                    CorrectOptionIndex = 1,
                    Explanation = "In C#, overloads must differ in the number, types, or order of their parameters; return type alone is not part of the signature."
                }
            }
        });

        // 37. Static Keyword
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-09",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 9,
            Title = "Static Keyword: Classes, Methods & Constants",
            Subtitle = "Shared class-level members vs instance members, and pure utility functions",
            ReadTimeMinutes = 8,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Static members belong to the Type itself, not to any specific instance.",
                "Static methods cannot access instance fields (this keyword is invalid in static context).",
                "Ideal for stateless mathematical formulas (Math.Sin, UnitUtils) and extension methods.",
                "Never store Revit Document or Element instances in static fields!"
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Pure Static Utilities",
                    ContentHtml = "<p>Pure functions take inputs, perform math, and return an answer without reading or modifying external state. These should always be <code>public static</code>.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Stateless Static Conversion Helper",
                    Description = "Pure static math utility class.",
                    Code = @"public static class BimMathUtils
{
    public const double FeetToMetersFactor = 0.3048;
    public const double SqFeetToSqMetersFactor = 0.092903;

    public static double FeetToMeters(double feet) => feet * FeetToMetersFactor;
    public static double MetersToFeet(double meters) => meters / FeetToMetersFactor;
}",
                    Explanation = "Can be invoked directly via BimMathUtils.FeetToMeters(10.0) without instantiation."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Storing ActiveUIDocument in Static Variable",
                    Description = "Holding a static reference to a Document prevents Revit from releasing its memory when the user closes the project.",
                    BadCode = @"public static class AppState {
    public static Document CurrentDoc; // LEAKS 2GB OF RAM WHEN DOCUMENT IS CLOSED!
}",
                    GoodCode = @"// Pass Document explicitly into methods:
public void Audit(Document doc) { /* ... */ }",
                    WhyItMatters = "Static variables outlive documents, causing severe memory exhaustion."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Why is it dangerous to store a Revit Document reference in a static field?",
                    Options = new() { "Revit automatically reboots Windows", "It creates a memory leak, preventing the closed document from being garbage collected", "Static variables cannot store objects", "It slows down CPU clock speed" },
                    CorrectOptionIndex = 1,
                    Explanation = "Static roots keep objects alive indefinitely, preventing .NET Garbage Collection even after the user closes the project."
                }
            }
        });

        // 38. Inheritance
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-10",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 10,
            Title = "Inheritance: Base Classes & Virtual Overrides",
            Subtitle = "Subclassing, 'base' keyword, virtual/override methods, and Revit's Element hierarchy",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "Inheritance allows a child class to inherit fields, properties, and methods from a parent base class.",
                "Revit's entire database is built on inheritance: Element -> HostObject -> Wall.",
                "Use 'virtual' on base methods to allow child classes to 'override' their behavior."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Revit's Core Inheritance Hierarchy",
                    ContentHtml = "<div class='p-3 bg-black rounded font-monospace small text-cyan'>Autodesk.Revit.DB.Element<br/>&nbsp;&nbsp;&boxur;&nbsp;HostObject<br/>&nbsp;&nbsp;&nbsp;&nbsp;&boxur;&nbsp;Wall<br/>&nbsp;&nbsp;&nbsp;&nbsp;&boxur;&nbsp;Floor<br/>&nbsp;&nbsp;&nbsp;&nbsp;&boxur;&nbsp;RoofBase<br/>&nbsp;&nbsp;&boxur;&nbsp;FamilyInstance<br/>&nbsp;&nbsp;&boxur;&nbsp;View</div>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Inheritance with Base Class Architecture",
                    Description = "Creating a custom command base class with automatic error logging.",
                    Code = @"// Reusable Base Command
public abstract class BaseRevitCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        try
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            return RunCommand(doc);
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return Result.Failed;
        }
    }

    // Abstract method that concrete child commands must implement
    protected abstract Result RunCommand(Document doc);
}

// Child Implementation:
public class AuditCommand : BaseRevitCommand
{
    protected override Result RunCommand(Document doc)
    {
        // Safe, clean execution with automatic try/catch wrapper!
        return Result.Succeeded;
    }
}",
                    Explanation = "Abstract base classes eliminate boilerplate across multiple commands."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Forgetting the 'override' Keyword",
                    Description = "Declaring a method in a child class with the same name without 'override' hides the base method instead of overriding it (warning CS0108).",
                    BadCode = @"public class Child : Parent {
    public void Process() { } // Warning: hides Parent.Process()
}",
                    GoodCode = @"public class Child : Parent {
    public override void Process() { } // Correctly overrides virtual method
}",
                    WhyItMatters = "Polymorphic calls will invoke the base implementation instead of the child."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What base class do Wall, Floor, FamilyInstance, and View all inherit from in the Revit API?",
                    Options = new() { "Autodesk.Revit.DB.Object", "Autodesk.Revit.DB.Element", "Autodesk.Revit.DB.GeometryObject", "System.Windows.DependencyObject" },
                    CorrectOptionIndex = 1,
                    Explanation = "In Autodesk Revit, virtually all building components in the database inherit from Autodesk.Revit.DB.Element."
                }
            }
        });

        // 39. Enumerations
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-11",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 11,
            Title = "Enumerations (enum): Named Constants",
            Subtitle = "BuiltInCategory, BuiltInParameter, StorageType, and custom enum definitions",
            ReadTimeMinutes = 7,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "An enum is a distinct value type consisting of a set of named integer constants.",
                "Revit API relies on huge enums: BuiltInCategory, BuiltInParameter, TransactionMode, Result.",
                "Enums prevent 'magic strings' and guarantee compile-time validation."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "The Power of BuiltInParameter",
                    ContentHtml = "<p>Instead of searching for parameter names as strings (which changes between German, French, and English Revit installations), use the <strong>BuiltInParameter</strong> enum. It uses immutable integer IDs that work globally on every machine!</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Using Enums vs Strings",
                    Description = "Language-independent parameter reading with BuiltInParameter.",
                    Code = @"// Language-independent, compiler-verified access:
Parameter? lengthParam = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);

// Custom enum for add-in execution modes:
public enum AuditSeverity
{
    Info,
    Warning,
    CriticalError
}",
                    Explanation = "Eliminates localization bugs completely."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Using String Names for Built-in Categories",
                    Description = "Comparing category by string name ('Walls') breaks on non-English versions of Revit.",
                    BadCode = @"if (elem.Category.Name == ""Walls"") { /* Fails in German Revit! */ }",
                    GoodCode = @"if (elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Walls) { /* Safe */ }",
                    WhyItMatters = "BuiltInCategory IDs never change regardless of user language."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Why is it best practice to read parameters using BuiltInParameter instead of string names?",
                    Options = new() { "It is 100% language-independent and works on all localized Revit installations", "Strings are banned in Revit", "It compresses the file size", "It encrypts the model" },
                    CorrectOptionIndex = 0,
                    Explanation = "BuiltInParameter enums refer to underlying integer IDs that remain consistent across English, German, French, and all Revit language editions."
                }
            }
        });

        // 40. Interface
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-12",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 12,
            Title = "Interfaces: Decoupled Architectural Contracts",
            Subtitle = "IExternalCommand, IExternalApplication, IFailuresPreprocessor, and ISelectionFilter",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "An interface defines a contract of method signatures, properties, and events without implementation.",
                "A class can implement multiple interfaces (unlike single class inheritance).",
                "Autodesk Revit's entire plugin architecture relies on interfaces for dependency inversion."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Key Revit API Interfaces",
                    ContentHtml = "<ul><li><code>IExternalCommand</code>: Entry point for user-triggered ribbon buttons.</li><li><code>IExternalApplication</code>: Startup/shutdown lifecycle and custom ribbon tabs.</li><li><code>IFailuresPreprocessor</code>: Intercepts and suppresses modal warnings.</li><li><code>ISelectionFilter</code>: Constrains interactive mouse picks (e.g. only allow picking walls).</li></ul>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Implementing ISelectionFilter",
                    Description = "Restricting user pick selection strictly to Wall instances.",
                    Code = @"using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

public class WallSelectionFilter : ISelectionFilter
{
    // Determines if an element can be selected:
    public bool AllowElement(Element elem)
    {
        return elem is Wall; // Only allow clicking on walls!
    }

    // Determines if a reference (face/edge) can be selected:
    public bool AllowReference(Reference reference, XYZ position)
    {
        return false;
    }
}",
                    Explanation = "Passed directly to uidoc.Selection.PickObject(ObjectType.Element, new WallSelectionFilter())."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Failing to Implement All Interface Members",
                    Description = "Omitting AllowReference on ISelectionFilter results in compiler error CS0535.",
                    BadCode = @"public class MyFilter : ISelectionFilter {
    public bool AllowElement(Element e) => true; // Error: missing AllowReference!
}",
                    GoodCode = @"public class MyFilter : ISelectionFilter {
    public bool AllowElement(Element e) => true;
    public bool AllowReference(Reference r, XYZ p) => false;
}",
                    WhyItMatters = "Classes must implement every member declared in an interface."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "Which Revit API interface is used to restrict user mouse picking to specific element types?",
                    Options = new() { "IExternalCommand", "ISelectionFilter", "IFamilyLoadOptions", "IUpdater" },
                    CorrectOptionIndex = 1,
                    Explanation = "ISelectionFilter defines AllowElement() and AllowReference() to filter what the user can click."
                }
            }
        });

        // 41. Exception Handling
        mod.Topics.Add(new Topic
        {
            Id = "c-adv-13",
            ModuleId = "c-adv",
            TrackId = "csharp",
            Order = 13,
            Title = "Exception Handling: Try, Catch, Finally & Logging",
            Subtitle = "Graceful degradation, OperationCanceledException, and preventing catastrophic session crashes",
            ReadTimeMinutes = 9,
            Level = DifficultyLevel.Intermediate,
            KeyTakeaways = new()
            {
                "An unhandled exception in an add-in will trigger Revit's fatal error dialog, risking data loss.",
                "Wrap command bodies in try-catch-finally blocks.",
                "Catch Autodesk.Revit.Exceptions.OperationCanceledException separately (triggered when user presses ESC).",
                "Return Result.Cancelled cleanly on ESC; return Result.Failed on actual errors."
            },
            Sections = new()
            {
                new TopicSection
                {
                    Title = "Handling the ESC Key in Revit",
                    ContentHtml = "<p>When you prompt a user to pick an element (<code>uidoc.Selection.PickObject</code>), pressing the <strong>ESC</strong> key throws an <code>OperationCanceledException</code>. This is NOT an error! You should catch it and return <code>Result.Cancelled</code> silently without showing an error popup.</p>"
                }
            },
            CodeSnippets = new()
            {
                new CodeSnippet
                {
                    Title = "Professional Production Try-Catch Envelope",
                    Description = "Standard industrial template for Revit commands.",
                    Code = @"public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
{
    try
    {
        // 1. Interactive selection:
        var uidoc = commandData.Application.ActiveUIDocument;
        Reference pickedRef = uidoc.Selection.PickObject(ObjectType.Element, ""Pick a wall"");
        
        // 2. Perform work...
        return Result.Succeeded;
    }
    catch (Autodesk.Revit.Exceptions.OperationCanceledException)
    {
        // User pressed ESC key: exit cleanly without displaying error
        return Result.Cancelled;
    }
    catch (Exception ex)
    {
        // Unexpected error: log details and show informative message
        message = ex.Message;
        TaskDialog.Show(""Command Error"", $""An error occurred:\n{ex.Message}\n\nStack:\n{ex.StackTrace}"");
        return Result.Failed;
    }
}",
                    Explanation = "Differentiates normal user cancellation from actual runtime crashes."
                }
            },
            Pitfalls = new()
            {
                new Pitfall
                {
                    Title = "Showing Error Dialog When User Cancels",
                    Description = "Treating OperationCanceledException as a fatal error displays annoying 'Error!' dialogs when user simply presses ESC.",
                    BadCode = @"catch (Exception ex) {
    TaskDialog.Show(""FATAL ERROR"", ex.Message); // Pops up even when user presses ESC!
}",
                    GoodCode = @"catch (OperationCanceledException) { return Result.Cancelled; }",
                    WhyItMatters = "Greatly improves user experience and matches native Revit behavior."
                }
            },
            Quizzes = new()
            {
                new QuizQuestion
                {
                    Question = "What should a Revit command return when catching an OperationCanceledException caused by the user pressing ESC?",
                    Options = new() { "Result.Failed", "Result.Succeeded", "Result.Cancelled", "throw new Exception()" },
                    CorrectOptionIndex = 2,
                    Explanation = "Returning Result.Cancelled tells Revit that the user purposefully aborted the command, terminating cleanly without error dialogs."
                }
            }
        });

        return mod;
    }
}
