namespace RevitApiMastery.Models;

public class BoilerplateConfig
{
    public string ProjectName { get; set; } = "BimEfficiencyTools";
    public string RevitVersion { get; set; } = "2025"; // "2024" or "2025"
    public string TargetFramework => RevitVersion == "2025" ? "net8.0-windows" : "net48";
    public bool IncludeRibbonApp { get; set; } = true;
    public bool IncludeWpfMvvm { get; set; } = true;
    public bool IncludeFailuresPreprocessor { get; set; } = true;
    public bool IncludeSelectionHelper { get; set; } = true;
    public string CompanyName { get; set; } = "BimStudios";
    public string AuthorName { get; set; } = "BimEngineer";
}
