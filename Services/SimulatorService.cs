using RevitApiMastery.Models;
using System.Diagnostics;

namespace RevitApiMastery.Services;

public class SimulatorService
{
    private readonly SimulatedDocument _document;

    public SimulatorService()
    {
        _document = SeedSampleModel();
    }

    public SimulatedDocument GetDocument() => _document;

    public QueryResult ExecuteQuery(
        string? category = null, 
        string? className = null, 
        bool? isNotElementType = true, 
        string? paramName = null, 
        string? paramFilter = null,
        string? level = null)
    {
        var sw = Stopwatch.StartNew();
        var logs = new List<string>();

        logs.Add(">>> Initializing FilteredElementCollector(doc)...");
        IEnumerable<SimulatedElement> query = _document.Elements;

        if (!string.IsNullOrWhiteSpace(category) && category != "All")
        {
            logs.Add($"Applied QuickFilter: .OfCategory(BuiltInCategory.OST_{category})");
            query = query.Where(e => string.Equals(e.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(className) && className != "All")
        {
            logs.Add($"Applied QuickFilter: .OfClass(typeof({className}))");
            query = query.Where(e => string.Equals(e.FamilyName, className, StringComparison.OrdinalIgnoreCase) || 
                                     e.Name.Contains(className, StringComparison.OrdinalIgnoreCase) ||
                                     e.Category.Contains(className, StringComparison.OrdinalIgnoreCase));
        }

        if (isNotElementType.HasValue)
        {
            if (isNotElementType.Value)
            {
                logs.Add("Applied QuickFilter: .WhereElementIsNotElementType()");
                query = query.Where(e => !e.IsElementType);
            }
            else
            {
                logs.Add("Applied QuickFilter: .WhereElementIsElementType()");
                query = query.Where(e => e.IsElementType);
            }
        }

        if (!string.IsNullOrWhiteSpace(level) && level != "All")
        {
            logs.Add($"Applied ParameterFilter: Level == '{level}'");
            query = query.Where(e => string.Equals(e.Level, level, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(paramName) && !string.IsNullOrWhiteSpace(paramFilter))
        {
            logs.Add($"Applied SlowFilter / Evaluator: LookupParameter(\"{paramName}\") matching \"{paramFilter}\"");
            query = query.Where(e =>
            {
                var p = e.LookupParameter(paramName);
                if (p == null || p.Value == null) return false;
                return p.Value.ToString()!.Contains(paramFilter, StringComparison.OrdinalIgnoreCase);
            });
        }

        var results = query.ToList();
        sw.Stop();
        logs.Add($"Collector Evaluated in {sw.ElapsedMilliseconds} ms. Matched {results.Count} elements out of {_document.Elements.Count} total.");

        return new QueryResult
        {
            FilterDescription = $"Category: {category ?? "All"}, Class: {className ?? "All"}, InstancesOnly: {isNotElementType?.ToString() ?? "Any"}",
            TotalElementsInModel = _document.Elements.Count,
            FilteredCount = results.Count,
            ExecutionTimeMs = sw.ElapsedMilliseconds,
            MatchedElements = results,
            ConsoleLogs = logs
        };
    }

    private static SimulatedDocument SeedSampleModel()
    {
        var doc = new SimulatedDocument { Title = "RevitApiMastery_OfficeTower.rvt", ActiveView = "{3D - Architecture}" };

        var elements = new List<SimulatedElement>
        {
            // Level Types & Instances
            new SimulatedElement
            {
                Id = 101, Name = "Level 1", Category = "Levels", FamilyName = "Level", TypeName = "8mm Head",
                Level = "Level 1", IsElementType = false,
                Parameters = new()
                {
                    ["Elevation"] = new() { Name = "Elevation", BuiltInName = "LEVEL_ELEV", StorageType = "Double", Value = 0.0, DisplayUnit = "0.00 m", IsReadOnly = false },
                    ["Name"] = new() { Name = "Name", BuiltInName = "DATUM_TEXT", StorageType = "String", Value = "Level 1", DisplayUnit = "", IsReadOnly = false }
                }
            },
            new SimulatedElement
            {
                Id = 102, Name = "Level 2", Category = "Levels", FamilyName = "Level", TypeName = "8mm Head",
                Level = "Level 2", IsElementType = false,
                Parameters = new()
                {
                    ["Elevation"] = new() { Name = "Elevation", BuiltInName = "LEVEL_ELEV", StorageType = "Double", Value = 13.1234, DisplayUnit = "4.00 m", IsReadOnly = false },
                    ["Name"] = new() { Name = "Name", BuiltInName = "DATUM_TEXT", StorageType = "String", Value = "Level 2", DisplayUnit = "", IsReadOnly = false }
                }
            },

            // Wall Types
            new SimulatedElement
            {
                Id = 201, Name = "Exterior - Brick on CMU", Category = "Walls", FamilyName = "Basic Wall", TypeName = "Exterior - Brick on CMU",
                IsElementType = true, Level = "",
                Parameters = new()
                {
                    ["Width"] = new() { Name = "Width", BuiltInName = "WALL_ATTR_WIDTH_PARAM", StorageType = "Double", Value = 1.148, DisplayUnit = "350 mm", IsReadOnly = true },
                    ["Function"] = new() { Name = "Function", BuiltInName = "FUNCTION_PARAM", StorageType = "Integer", Value = 1, DisplayUnit = "Exterior", IsReadOnly = false },
                    ["Thermal Resistance (R)"] = new() { Name = "Thermal Resistance (R)", BuiltInName = "ALL_MODEL_THERMAL_RESISTANCE", StorageType = "Double", Value = 3.25, DisplayUnit = "m²·K/W", IsReadOnly = true }
                }
            },
            new SimulatedElement
            {
                Id = 202, Name = "Interior - 100mm Partition", Category = "Walls", FamilyName = "Basic Wall", TypeName = "Interior - 100mm Partition",
                IsElementType = true, Level = "",
                Parameters = new()
                {
                    ["Width"] = new() { Name = "Width", BuiltInName = "WALL_ATTR_WIDTH_PARAM", StorageType = "Double", Value = 0.328, DisplayUnit = "100 mm", IsReadOnly = true },
                    ["Function"] = new() { Name = "Function", BuiltInName = "FUNCTION_PARAM", StorageType = "Integer", Value = 0, DisplayUnit = "Interior", IsReadOnly = false }
                }
            },

            // Wall Instances
            new SimulatedElement
            {
                Id = 5001, Name = "Basic Wall [Exterior - Brick on CMU]", Category = "Walls", FamilyName = "Basic Wall", TypeName = "Exterior - Brick on CMU",
                Level = "Level 1", IsElementType = false,
                BoundingBoxMin = (0, 0, 0), BoundingBoxMax = (30, 0.35, 4),
                Parameters = new()
                {
                    ["Length"] = new() { Name = "Length", BuiltInName = "CURVE_ELEM_LENGTH", StorageType = "Double", Value = 98.425, DisplayUnit = "30.00 m", IsReadOnly = true },
                    ["Area"] = new() { Name = "Area", BuiltInName = "HOST_AREA_COMPUTED", StorageType = "Double", Value = 1291.67, DisplayUnit = "120.00 m²", IsReadOnly = true },
                    ["Volume"] = new() { Name = "Volume", BuiltInName = "HOST_VOLUME_COMPUTED", StorageType = "Double", Value = 1483.22, DisplayUnit = "42.00 m³", IsReadOnly = true },
                    ["Comments"] = new() { Name = "Comments", BuiltInName = "ALL_MODEL_INSTANCE_COMMENTS", StorageType = "String", Value = "North Facade", IsReadOnly = false },
                    ["Mark"] = new() { Name = "Mark", BuiltInName = "ALL_MODEL_MARK", StorageType = "String", Value = "W-01", IsReadOnly = false }
                }
            },
            new SimulatedElement
            {
                Id = 5002, Name = "Basic Wall [Exterior - Brick on CMU]", Category = "Walls", FamilyName = "Basic Wall", TypeName = "Exterior - Brick on CMU",
                Level = "Level 1", IsElementType = false,
                BoundingBoxMin = (0, 0, 0), BoundingBoxMax = (0.35, 20, 4),
                Parameters = new()
                {
                    ["Length"] = new() { Name = "Length", BuiltInName = "CURVE_ELEM_LENGTH", StorageType = "Double", Value = 65.616, DisplayUnit = "20.00 m", IsReadOnly = true },
                    ["Area"] = new() { Name = "Area", BuiltInName = "HOST_AREA_COMPUTED", StorageType = "Double", Value = 861.11, DisplayUnit = "80.00 m²", IsReadOnly = true },
                    ["Volume"] = new() { Name = "Volume", BuiltInName = "HOST_VOLUME_COMPUTED", StorageType = "Double", Value = 988.81, DisplayUnit = "28.00 m³", IsReadOnly = true },
                    ["Comments"] = new() { Name = "Comments", BuiltInName = "ALL_MODEL_INSTANCE_COMMENTS", StorageType = "String", Value = "West Facade", IsReadOnly = false },
                    ["Mark"] = new() { Name = "Mark", BuiltInName = "ALL_MODEL_MARK", StorageType = "String", Value = "W-02", IsReadOnly = false }
                }
            },
            new SimulatedElement
            {
                Id = 5003, Name = "Basic Wall [Interior - 100mm Partition]", Category = "Walls", FamilyName = "Basic Wall", TypeName = "Interior - 100mm Partition",
                Level = "Level 1", IsElementType = false,
                BoundingBoxMin = (10, 0, 0), BoundingBoxMax = (10.1, 15, 3.5),
                Parameters = new()
                {
                    ["Length"] = new() { Name = "Length", BuiltInName = "CURVE_ELEM_LENGTH", StorageType = "Double", Value = 49.212, DisplayUnit = "15.00 m", IsReadOnly = true },
                    ["Area"] = new() { Name = "Area", BuiltInName = "HOST_AREA_COMPUTED", StorageType = "Double", Value = 565.10, DisplayUnit = "52.50 m²", IsReadOnly = true },
                    ["Comments"] = new() { Name = "Comments", BuiltInName = "ALL_MODEL_INSTANCE_COMMENTS", StorageType = "String", Value = "Corridor divider", IsReadOnly = false },
                    ["Mark"] = new() { Name = "Mark", BuiltInName = "ALL_MODEL_MARK", StorageType = "String", Value = "W-INT-01", IsReadOnly = false }
                }
            },
            new SimulatedElement
            {
                Id = 5004, Name = "Basic Wall [Exterior - Brick on CMU]", Category = "Walls", FamilyName = "Basic Wall", TypeName = "Exterior - Brick on CMU",
                Level = "Level 2", IsElementType = false,
                BoundingBoxMin = (0, 0, 4), BoundingBoxMax = (30, 0.35, 8),
                Parameters = new()
                {
                    ["Length"] = new() { Name = "Length", BuiltInName = "CURVE_ELEM_LENGTH", StorageType = "Double", Value = 98.425, DisplayUnit = "30.00 m", IsReadOnly = true },
                    ["Area"] = new() { Name = "Area", BuiltInName = "HOST_AREA_COMPUTED", StorageType = "Double", Value = 1291.67, DisplayUnit = "120.00 m²", IsReadOnly = true },
                    ["Comments"] = new() { Name = "Comments", BuiltInName = "ALL_MODEL_INSTANCE_COMMENTS", StorageType = "String", Value = "Level 2 North Facade", IsReadOnly = false },
                    ["Mark"] = new() { Name = "Mark", BuiltInName = "ALL_MODEL_MARK", StorageType = "String", Value = "W-03", IsReadOnly = false }
                }
            },

            // Doors
            new SimulatedElement
            {
                Id = 6001, Name = "Single-Flush [0915 x 2134 mm]", Category = "Doors", FamilyName = "Single-Flush", TypeName = "0915 x 2134 mm",
                Level = "Level 1", IsElementType = false,
                Parameters = new()
                {
                    ["Mark"] = new() { Name = "Mark", BuiltInName = "ALL_MODEL_MARK", StorageType = "String", Value = "D-101", IsReadOnly = false },
                    ["Width"] = new() { Name = "Width", BuiltInName = "DOOR_WIDTH", StorageType = "Double", Value = 3.0, DisplayUnit = "915 mm", IsReadOnly = true },
                    ["Height"] = new() { Name = "Height", BuiltInName = "DOOR_HEIGHT", StorageType = "Double", Value = 7.0, DisplayUnit = "2134 mm", IsReadOnly = true },
                    ["Fire Rating"] = new() { Name = "Fire Rating", BuiltInName = "FIRE_RATING", StorageType = "String", Value = "60 min", IsReadOnly = false },
                    ["Comments"] = new() { Name = "Comments", BuiltInName = "ALL_MODEL_INSTANCE_COMMENTS", StorageType = "String", Value = "Main entrance corridor", IsReadOnly = false }
                }
            },
            new SimulatedElement
            {
                Id = 6002, Name = "Single-Flush [0915 x 2134 mm]", Category = "Doors", FamilyName = "Single-Flush", TypeName = "0915 x 2134 mm",
                Level = "Level 1", IsElementType = false,
                Parameters = new()
                {
                    ["Mark"] = new() { Name = "Mark", BuiltInName = "ALL_MODEL_MARK", StorageType = "String", Value = "D-102", IsReadOnly = false },
                    ["Width"] = new() { Name = "Width", BuiltInName = "DOOR_WIDTH", StorageType = "Double", Value = 3.0, DisplayUnit = "915 mm", IsReadOnly = true },
                    ["Height"] = new() { Name = "Height", BuiltInName = "DOOR_HEIGHT", StorageType = "Double", Value = 7.0, DisplayUnit = "2134 mm", IsReadOnly = true },
                    ["Fire Rating"] = new() { Name = "Fire Rating", BuiltInName = "FIRE_RATING", StorageType = "String", Value = "30 min", IsReadOnly = false }
                }
            },

            // Windows
            new SimulatedElement
            {
                Id = 7001, Name = "Fixed [0915 x 1220 mm]", Category = "Windows", FamilyName = "Fixed", TypeName = "0915 x 1220 mm",
                Level = "Level 1", IsElementType = false,
                Parameters = new()
                {
                    ["Mark"] = new() { Name = "Mark", BuiltInName = "ALL_MODEL_MARK", StorageType = "String", Value = "WIN-01", IsReadOnly = false },
                    ["Sill Height"] = new() { Name = "Sill Height", BuiltInName = "INSTANCE_SILL_HEIGHT_PARAM", StorageType = "Double", Value = 2.95, DisplayUnit = "900 mm", IsReadOnly = false }
                }
            },
            new SimulatedElement
            {
                Id = 7002, Name = "Fixed [0915 x 1220 mm]", Category = "Windows", FamilyName = "Fixed", TypeName = "0915 x 1220 mm",
                Level = "Level 2", IsElementType = false,
                Parameters = new()
                {
                    ["Mark"] = new() { Name = "Mark", BuiltInName = "ALL_MODEL_MARK", StorageType = "String", Value = "WIN-02", IsReadOnly = false },
                    ["Sill Height"] = new() { Name = "Sill Height", BuiltInName = "INSTANCE_SILL_HEIGHT_PARAM", StorageType = "Double", Value = 2.95, DisplayUnit = "900 mm", IsReadOnly = false }
                }
            },

            // Rooms
            new SimulatedElement
            {
                Id = 8001, Name = "Conference Room 101", Category = "Rooms", FamilyName = "Room", TypeName = "Standard Room",
                Level = "Level 1", IsElementType = false,
                Parameters = new()
                {
                    ["Number"] = new() { Name = "Number", BuiltInName = "ROOM_NUMBER", StorageType = "String", Value = "101", IsReadOnly = false },
                    ["Name"] = new() { Name = "Name", BuiltInName = "ROOM_NAME", StorageType = "String", Value = "Conference Room", IsReadOnly = false },
                    ["Area"] = new() { Name = "Area", BuiltInName = "ROOM_AREA", StorageType = "Double", Value = 484.376, DisplayUnit = "45.00 m²", IsReadOnly = true },
                    ["Occupancy"] = new() { Name = "Occupancy", BuiltInName = "ROOM_OCCUPANCY", StorageType = "String", Value = "20 Persons", IsReadOnly = false }
                }
            },
            new SimulatedElement
            {
                Id = 8002, Name = "Open Office 102", Category = "Rooms", FamilyName = "Room", TypeName = "Standard Room",
                Level = "Level 1", IsElementType = false,
                Parameters = new()
                {
                    ["Number"] = new() { Name = "Number", BuiltInName = "ROOM_NUMBER", StorageType = "String", Value = "102", IsReadOnly = false },
                    ["Name"] = new() { Name = "Name", BuiltInName = "ROOM_NAME", StorageType = "String", Value = "Open Office", IsReadOnly = false },
                    ["Area"] = new() { Name = "Area", BuiltInName = "ROOM_AREA", StorageType = "Double", Value = 1291.669, DisplayUnit = "120.00 m²", IsReadOnly = true }
                }
            },

            // Structural Columns
            new SimulatedElement
            {
                Id = 9001, Name = "M_Concrete-Rectangular-Column [450 x 450mm]", Category = "Structural Columns", FamilyName = "M_Concrete-Rectangular-Column", TypeName = "450 x 450mm",
                Level = "Level 1", IsElementType = false,
                Parameters = new()
                {
                    ["Mark"] = new() { Name = "Mark", BuiltInName = "ALL_MODEL_MARK", StorageType = "String", Value = "C-1", IsReadOnly = false },
                    ["Base Level"] = new() { Name = "Base Level", BuiltInName = "COLUMN_BASE_LEVEL_PARAM", StorageType = "String", Value = "Level 1", IsReadOnly = false },
                    ["Top Level"] = new() { Name = "Top Level", BuiltInName = "COLUMN_TOP_LEVEL_PARAM", StorageType = "String", Value = "Level 2", IsReadOnly = false }
                }
            },

            // MEP Ducts & Air Terminals
            new SimulatedElement
            {
                Id = 9501, Name = "Supply Diffuser [600 x 600 Face]", Category = "Air Terminals", FamilyName = "Supply Diffuser", TypeName = "600 x 600 Face",
                Level = "Level 1", IsElementType = false,
                Parameters = new()
                {
                    ["Flow"] = new() { Name = "Flow", BuiltInName = "RBS_DUCT_FLOW_PARAM", StorageType = "Double", Value = 250.0, DisplayUnit = "250 L/s", IsReadOnly = false },
                    ["System Type"] = new() { Name = "System Type", BuiltInName = "RBS_DUCT_SYSTEM_TYPE_PARAM", StorageType = "String", Value = "Supply Air", IsReadOnly = true }
                }
            }
        };

        doc.Elements.AddRange(elements);
        return doc;
    }
}
