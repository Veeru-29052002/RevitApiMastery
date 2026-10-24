namespace RevitApiMastery.Models;

public class ManifestConfig
{
    public string AddinType { get; set; } = "Command"; // "Command" or "Application"
    public string AddinName { get; set; } = "BimAutomationTool";
    public string AssemblyPath { get; set; } = @"C:\ProgramData\Autodesk\Revit\Addins\2025\BimAutomationTool\BimAutomationTool.dll";
    public string FullClassName { get; set; } = "BimAutomationTool.Commands.ExportDataCommand";
    public string AddinId { get; set; } = Guid.NewGuid().ToString().ToUpper();
    public string VendorId { get; set; } = "AUTR";
    public string VendorDescription { get; set; } = "Autodesk Developer Network / BIM Developer";
    public string Description { get; set; } = "Automated BIM utility for model validation and quantity extraction.";
    public string VisibilityMode { get; set; } = "AlwaysVisible"; // AlwaysVisible, NotVisibleWhenNoActiveDocument, etc.
    public string Discipline { get; set; } = "Any"; // Any, Architecture, Structure, Mechanical, Electrical, Piping
}
