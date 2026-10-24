namespace RevitApiMastery.Models;

public class SimulatedParameter
{
    public string Name { get; set; } = string.Empty;
    public string BuiltInName { get; set; } = string.Empty;
    public string StorageType { get; set; } = "String"; // String, Double, Integer, ElementId
    public object? Value { get; set; }
    public string DisplayUnit { get; set; } = string.Empty;
    public bool IsReadOnly { get; set; }
}

public class SimulatedElement
{
    public int Id { get; set; }
    public string UniqueId { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string TypeName { get; set; } = string.Empty;
    public string Level { get; set; } = "Level 1";
    public bool IsElementType { get; set; }
    public (double X, double Y, double Z) BoundingBoxMin { get; set; }
    public (double X, double Y, double Z) BoundingBoxMax { get; set; }
    public Dictionary<string, SimulatedParameter> Parameters { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public SimulatedParameter? LookupParameter(string paramName)
    {
        if (Parameters.TryGetValue(paramName, out var param))
            return param;
        return null;
    }
}

public class SimulatedDocument
{
    public string Title { get; set; } = "SampleCommercialBuilding.rvt";
    public string ActiveView { get; set; } = "{3D - Coordination}";
    public List<SimulatedElement> Elements { get; set; } = new();
}

public class QueryResult
{
    public string FilterDescription { get; set; } = string.Empty;
    public int TotalElementsInModel { get; set; }
    public int FilteredCount { get; set; }
    public long ExecutionTimeMs { get; set; }
    public List<SimulatedElement> MatchedElements { get; set; } = new();
    public List<string> ConsoleLogs { get; set; } = new();
}
