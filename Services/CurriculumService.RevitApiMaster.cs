using RevitApiMastery.Models;

namespace RevitApiMastery.Services;

public partial class CurriculumService
{
    private Module BuildRevitApiMasterModule()
    {
        var mod = new Module
        {
            Id = "revit-master",
            TrackId = "revitapi",
            Order = 2,
            LevelNumber = 4,
            LevelTitle = "Stage 4: Revit API Master (WPF, MVVM & Projects)",
            Title = "Revit API Stage 4: WPF, MVVM & Enterprise Projects",
            ShortDescription = "Complete modern UI with WPF XAML, styles, triggers, Material Design, MVVM, and 2 full end-to-end guided projects (Model Navigator & Room Wizard).",
            Icon = "bi-trophy",
            Level = DifficultyLevel.Expert
        };

        // Helper to add topic cleanly
        void AddTopic(string id, int order, string title, string sub, int time, string[] takeaways, string html, string snippetTitle, string snippetCode, string pitfallTitle, string badCode, string goodCode, string why, string quizQ, string[] quizOpts, int quizIdx, string quizExp)
        {
            mod.Topics.Add(new Topic
            {
                Id = id,
                ModuleId = "revit-master",
                TrackId = "revitapi",
                Order = order,
                Title = title,
                Subtitle = sub,
                ReadTimeMinutes = time,
                Level = DifficultyLevel.Advanced,
                KeyTakeaways = takeaways.ToList(),
                Sections = new() { new TopicSection { Title = title, ContentHtml = html } },
                CodeSnippets = new() { new CodeSnippet { Title = snippetTitle, Code = snippetCode, Description = "Production C# / XAML Implementation" } },
                Pitfalls = new() { new Pitfall { Title = pitfallTitle, BadCode = badCode, GoodCode = goodCode, WhyItMatters = why } },
                Quizzes = new() { new QuizQuestion { Question = quizQ, Options = quizOpts.ToList(), CorrectOptionIndex = quizIdx, Explanation = quizExp } }
            });
        }

        // 62. Introduction (WPF)
        AddTopic(
            "r-m-01", 1, "Introduction to WPF in Revit Add-ins",
            "Why Windows Presentation Foundation (WPF) and XAML replaced Windows Forms in professional BIM plugins", 8,
            new[] { "WPF decouples UI markup (XAML) from business logic (C#).", "Enables vector-based resolution-independent scaling (essential for 4K monitors).", "Native support for data binding, templates, and the MVVM architecture." },
            "<p>WinForms uses pixel-based GDI rendering from 2001 that blurs on high-DPI 4K monitors. <strong>WPF (Windows Presentation Foundation)</strong> uses DirectX hardware acceleration and vector graphics, making your Revit plugin look as sleek and modern as commercial software.</p>",
            "Enabling WPF in .csproj",
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF> <!-- Enables WPF and XAML compiler -->
  </PropertyGroup>
</Project>",
            "Mixing WinForms and WPF threads incorrectly",
            "System.Windows.Forms.Application.Run(new Form1()); // Blocks Revit message pump!",
            "var window = new ModernWpfWindow(); window.ShowDialog(); // Clean modal execution",
            "WinForms application loops can freeze Revit's internal message routing.",
            "What property in an SDK-style .csproj enables WPF compilation for Revit add-ins?",
            new[] { "<UseWPF>true</UseWPF>", "<EnableUI>true</EnableUI>", "<WpfMode>On</WpfMode>", "<DesktopApp>true</DesktopApp>" }, 0,
            "<UseWPF>true</UseWPF> instructs the .NET compiler to include PresentationCore, PresentationFramework, and the XAML compiler."
        );

        // 63. Testing Ways (WPF)
        AddTopic(
            "r-m-02", 2, "Testing Ways for WPF Revit Add-ins",
            "Modal ShowDialog() vs Modeless Show() vs Standalone Test Harness", 8,
            new[] { "Modal (ShowDialog): Blocks Revit until the window closes; execution context is 100% valid.", "Modeless (Show): Floats freely, but requires IExternalEventHandler to safely modify Revit.", "Standalone Test App: Build a WPF .exe with mock data to iterate 10x faster without opening Revit." },
            "<p>Don't launch Revit just to tweak button margins! Create a simple WPF Console/App test harness that binds mock Revit data to your ViewModels. Once the UI looks perfect, integrate it into your Revit class library.</p>",
            "Modal vs Modeless Window Launch",
            @"// Modal Execution (Blocks Revit UI, safe direct transactions):
var window = new AuditorWindow();
bool? result = window.ShowDialog();

// Modeless Execution (Floats freely, requires ExternalEvent for transactions):
var modelessWindow = new ModelessPalette();
modelessWindow.Show();",
            "Starting Transaction from Modeless Window Click",
            "// Inside Modeless Window Button Click:\nusing (var t = new Transaction(doc)) { t.Start(); } // CRASH!",
            "// Inside Modeless Window: Raise ExternalEvent\n_externalEvent.Raise();",
            "Modeless windows run on their own message loop and do not possess Revit's active API transaction context.",
            "Which method launches a WPF window modally, blocking interaction with Revit until the window closes?",
            new[] { "window.Show()", "window.ShowDialog()", "window.Open()", "window.RunModal()" }, 1,
            "window.ShowDialog() opens a modal dialog that pauses code execution until dismissed."
        );

        // 64. Window (WPF)
        AddTopic(
            "r-m-03", 3, "WPF Window Architecture & Revit Interop",
            "WindowChrome, WindowStartupLocation, and setting Revit as Owner window", 8,
            new[] { "Always set WindowStartupLocation = CenterScreen or CenterOwner.", "Set Revit's main HWND as Owner to prevent your window from hiding behind Revit.", "Use WindowInteropHelper to bridge Revit's native window handle to WPF." },
            "<p>Without setting Revit as the owner window, clicking anywhere in Revit can push your add-in window behind Revit, making the user think Revit has frozen!</p>",
            "Setting Revit Main Window as WPF Owner",
            @"using System.Windows.Interop;

public void ShowAddinWindow(UIApplication uiapp)
{
    var window = new AuditWindow();

    // Attach Revit's main process window handle as owner:
    var helper = new WindowInteropHelper(window);
    helper.Owner = uiapp.MainWindowHandle;

    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
    window.ShowDialog();
}",
            "Floating Add-in Hiding Behind Revit",
            "var win = new Window(); win.Show(); // Sinks behind Revit when user clicks viewport!",
            "new WindowInteropHelper(win).Owner = uiapp.MainWindowHandle; // Stays on top!",
            "Setting Owner ensures proper Z-order and minimizes along with Revit.",
            "What class is used to assign Revit's MainWindowHandle as the owner of a WPF Window?",
            new[] { "WindowInteropHelper", "ProcessManager", "RevitWindowConnector", "HwndBridge" }, 0,
            "WindowInteropHelper from System.Windows.Interop assigns native Win32 HWND pointers to WPF windows."
        );

        // 65. Button (WPF)
        AddTopic(
            "r-m-04", 4, "WPF Button Control & Styling",
            "Click events, Command binding, IsDefault, and IsCancel properties", 7,
            new[] { "IsDefault = true triggers button click when user presses ENTER.", "IsCancel = true triggers button click when user presses ESC and closes dialog.", "Use MVVM Command binding instead of code-behind Click handlers." },
            "<p>Buttons in WPF support full content composition: a button can contain an icon, a text label, and a progress indicator all together.</p>",
            "XAML Button with IsDefault and IsCancel",
            @"<StackPanel Orientation=""Horizontal"" HorizontalAlignment=""Right"" Margin=""10"">
    <!-- Primary action triggered by ENTER -->
    <Button Content=""Apply Changes"" Command=""{Binding ApplyCommand}"" IsDefault=""True"" Margin=""0,0,10,0""/>
    <!-- Cancel action triggered by ESC -->
    <Button Content=""Cancel"" IsCancel=""True""/>
</StackPanel>",
            "Ignoring IsDefault and IsCancel",
            "<!-- User presses Enter or ESC and nothing happens -->",
            "<Button Content=\"OK\" IsDefault=\"True\"/><Button Content=\"Cancel\" IsCancel=\"True\"/>",
            "IsDefault and IsCancel provide natural keyboard navigation expected in CAD tools.",
            "What property makes a WPF Button trigger automatically when the user presses the ESC key?",
            new[] { "IsDefault", "IsCancel", "AutoClose", "EscEnabled" }, 1,
            "Setting IsCancel='True' binds the button directly to the ESC key."
        );

        // 66. Layouts (WPF)
        AddTopic(
            "r-m-05", 5, "WPF Layout System: Principles & Containers",
            "Resolution independence, measure and arrange passes, and container choices", 8,
            new[] { "Never hardcode pixel Width/Height on windows and controls (breaks on DPI scaling).", "Use adaptive layout containers: Grid, StackPanel, DockPanel.", "Margin and Padding provide breathing room; HorizontalAlignment and VerticalAlignment control docking." },
            "<p>WPF computes layouts using a two-pass architecture: <strong>Measure</strong> (children report desired size) followed by <strong>Arrange</strong> (parents allocate actual boundaries). Hardcoded coordinates break this system.</p>",
            "Adaptive Fluid Layout",
            @"<!-- Responsive Container without hardcoded dimensions -->
<Grid Margin=""16"">
    <Grid.RowDefinitions>
        <RowDefinition Height=""Auto""/>    <!-- Takes only content size -->
        <RowDefinition Height=""*""/>       <!-- Takes all remaining space -->
        <RowDefinition Height=""Auto""/>    <!-- Takes only button size -->
    </Grid.RowDefinitions>
</Grid>",
            "Hardcoded Coordinates (WinForms style)",
            "<Button Width=\"100\" Height=\"30\" Margin=\"142,204,0,0\"/> <!-- Clips on 4K scaling! -->",
            "<Button Content=\"Submit\" HorizontalAlignment=\"Stretch\"/>",
            "Hardcoded margins and dimensions clip text on high-DPI displays.",
            "What row height setting in a WPF Grid allows a row to consume all remaining available space?",
            new[] { "Height='Auto'", "Height='*'", "Height='Fill'", "Height='100%'" }, 1,
            "The asterisk ('*') specifies proportional (star) sizing, absorbing all remaining space."
        );

        // 67. StackPanel
        AddTopic(
            "r-m-06", 6, "StackPanel: Sequential Element Stacking",
            "Vertical and Horizontal orientations for simple toolbars and form rows", 6,
            new[] { "Stacks child elements sequentially in a single direction (Vertical or Horizontal).", "Does not stretch children across both dimensions.", "Perfect for button toolbars and vertical input fields." },
            "<p><code>&lt;StackPanel Orientation=\"Vertical\"&gt;</code> is the simplest container in WPF. Elements simply stack one after another.</p>",
            "Horizontal Action Toolbar",
            @"<StackPanel Orientation=""Horizontal"" HorizontalAlignment=""Right"" Margin=""12"">
    <Button Content=""Export"" Width=""90"" Margin=""0,0,8,0""/>
    <Button Content=""Refresh"" Width=""90""/>
</StackPanel>",
            "Using StackPanel for Data Grids",
            "<StackPanel><DataGrid/></StackPanel> <!-- DataGrid loses its scrollbar! -->",
            "<Grid><DataGrid/></Grid> <!-- Grid constrains height and enables scrollbars -->",
            "StackPanel gives children infinite height, breaking scrollable controls.",
            "What happens if you place a scrollable DataGrid inside a vertical StackPanel without fixed height?",
            new[] { "It scrolls normally", "It expands infinitely and loses its vertical scrollbar", "It throws a compilation error", "It centers on screen" }, 1,
            "StackPanel provides infinite space in its stacking direction, causing scrollable controls to never display scrollbars."
        );

        // 68. DockPanel
        AddTopic(
            "r-m-07", 7, "DockPanel: Edge-Based Docking",
            "DockPanel.Dock Top, Bottom, Left, Right and LastChildFill", 7,
            new[] { "Docks child elements against the 4 outer edges of the container.", "LastChildFill = true (default) stretches the final child across all remaining center space.", "Ideal for IDE layouts with top toolbars, bottom status bars, and center workspaces." },
            "<p>DockPanel is the standard container for building Revit sidebar panes (docking search at top, status at bottom, and element list in the middle).</p>",
            "Standard Add-in DockPanel Shell",
            @"<DockPanel LastChildFill=""True"">
    <!-- Top toolbar -->
    <ToolBar DockPanel.Dock=""Top""> ... </ToolBar>
    <!-- Bottom status bar -->
    <StatusBar DockPanel.Dock=""Bottom"">
        <TextBlock Text=""Ready""/>
    </StatusBar>
    <!-- Center fills remainder -->
    <ListBox ItemsSource=""{Binding WallItems}""/>
</DockPanel>",
            "Forgetting Dock Order Matters",
            "<!-- Order dictates which element consumes the full edge! -->",
            "<StatusBar DockPanel.Dock=\"Bottom\"/><ToolBar DockPanel.Dock=\"Top\"/>",
            "Elements defined earlier in XAML consume the entire edge length.",
            "What does the LastChildFill property do in a WPF DockPanel?",
            new[] { "Hides the last child", "Stretches the final child element to fill all remaining interior space", "Docks it to the bottom", "Reverses layout order" }, 1,
            "LastChildFill automatically expands the last child to occupy the central space."
        );

        // 69. Grid
        AddTopic(
            "r-m-08", 8, "Grid: The Powerhouse Layout Engine",
            "ColumnDefinitions, RowDefinitions, Grid.Row, Grid.Column, and Grid.RowSpan", 8,
            new[] { "Grid is the most powerful and versatile layout container in WPF.", "Supports Absolute (px), Auto (content size), and Proportional (*) sizing.", "Grid.RowSpan and Grid.ColumnSpan allow elements to span across multiple cells." },
            "<p>90% of professional Revit add-in dialogs use a <code>Grid</code> as their root container. It maintains perfect alignment across form labels and inputs.</p>",
            "Two-Column Form Grid Layout",
            @"<Grid Margin=""16"">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width=""Auto""/>  <!-- Labels Column -->
        <ColumnDefinition Width=""*""/>     <!-- Inputs Column -->
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
        <RowDefinition Height=""Auto""/>
        <RowDefinition Height=""Auto""/>
    </Grid.RowDefinitions>

    <TextBlock Grid.Row=""0"" Grid.Column=""0"" Text=""Wall Type:"" VerticalAlignment=""Center""/>
    <ComboBox Grid.Row=""0"" Grid.Column=""1"" Margin=""8,4""/>

    <TextBlock Grid.Row=""1"" Grid.Column=""0"" Text=""Comments:"" VerticalAlignment=""Center""/>
    <TextBox Grid.Row=""1"" Grid.Column=""1"" Margin=""8,4""/>
</Grid>",
            "Omitting Grid.Row / Grid.Column",
            "<TextBlock Text=\"A\"/><TextBlock Text=\"B\"/> <!-- Both stack on (0,0) overlapping! -->",
            "<TextBlock Grid.Row=\"0\" Text=\"A\"/><TextBlock Grid.Row=\"1\" Text=\"B\"/>",
            "Omitting cell coordinates causes all elements to default to row 0, column 0.",
            "What is the default Grid.Row and Grid.Column if not explicitly specified?",
            new[] { "Row 0, Column 0", "Row 1, Column 1", "Auto-incremented", "Null" }, 0,
            "Elements default to cell (0,0) if Grid.Row and Grid.Column are omitted."
        );

        // 70. TextBlock
        AddTopic(
            "r-m-09", 9, "TextBlock: High-Performance Read-Only Text",
            "Typography, text trimming, text wrapping, and data binding", 6,
            new[] { "TextBlock is lightweight read-only text (much faster than a read-only TextBox).", "TextTrimming='CharacterEllipsis' adds '...' if the text overflows.", "TextWrapping='Wrap' wraps long descriptions onto multiple lines." },
            "<p>Use <code>TextBlock</code> everywhere you need labels, status indicators, or titles. Never use a disabled <code>TextBox</code> just to display text.</p>",
            "Formatted Ellipsis TextBlock",
            @"<TextBlock Text=""{Binding ElementLongName}"" 
           TextTrimming=""CharacterEllipsis"" 
           FontSize=""14"" 
           FontWeight=""SemiBold"" 
           Foreground=""#00EBFF"" 
           ToolTip=""{Binding ElementLongName}""/>",
            "Using TextBox Instead of TextBlock for Labels",
            "<TextBox Text=\"Select Wall:\" IsReadOnly=\"True\" BorderThickness=\"0\"/> <!-- Heavyweight! -->",
            "<TextBlock Text=\"Select Wall:\"/> <!-- Lightweight & Clean -->",
            "TextBox contains caret, selection, and undo buffers that waste memory for simple labels.",
            "What property adds '...' when text exceeds the width of a TextBlock?",
            new[] { "TextOverflow='Dots'", "TextTrimming='CharacterEllipsis'", "TextWrap='Truncate'", "ClipToBounds='True'" }, 1,
            "TextTrimming='CharacterEllipsis' truncates overflowing text with an ellipsis."
        );

        // 71. TextBox
        AddTopic(
            "r-m-10", 10, "TextBox: User Input & Live Validation",
            "Two-way binding, UpdateSourceTrigger=PropertyChanged, and input sanitization", 7,
            new[] { "TextBox accepts editable keyboard input.", "Default binding updates when focus is lost (UpdateSourceTrigger=LostFocus).", "Use UpdateSourceTrigger=PropertyChanged for instant search filtering as the user types." },
            "<p>For search boxes filtering 10,000 Revit elements, bind with <code>UpdateSourceTrigger=PropertyChanged</code> so the filter updates with every keystroke.</p>",
            "Instant Search TextBox",
            @"<TextBox Text=""{Binding SearchQuery, UpdateSourceTrigger=PropertyChanged}""
         Padding=""6""
         VerticalContentAlignment=""Center""
         Margin=""0,0,0,10""/>",
            "Forgetting UpdateSourceTrigger on Search Inputs",
            "<TextBox Text=\"{Binding SearchFilter}\"/> <!-- Only updates when user clicks away! -->",
            "<TextBox Text=\"{Binding SearchFilter, UpdateSourceTrigger=PropertyChanged}\"/>",
            "LostFocus forces users to click outside the textbox before searches take effect.",
            "What UpdateSourceTrigger mode updates the ViewModel property on every single keystroke?",
            new[] { "LostFocus", "PropertyChanged", "Explicit", "Default" }, 1,
            "UpdateSourceTrigger=PropertyChanged updates the underlying property immediately with every keystroke."
        );

        // 72. CheckBox
        AddTopic(
            "r-m-11", 11, "CheckBox: Boolean Options & Tri-State",
            "IsChecked boolean binding and ThreeState nullability for mixed element selections", 6,
            new[] { "CheckBox binds to bool or bool? (nullable boolean).", "IsThreeState = true allows Checked (true), Unchecked (false), and Indeterminate (null).", "Indeterminate is ideal for 'Select All' when only some elements are selected." },
            "<p>When selecting elements in a list, an 'All' checkbox can show a dash (null) if 3 out of 10 walls are selected.</p>",
            "Tri-State CheckBox",
            @"<CheckBox Content=""Select All Levels"" 
          IsThreeState=""True"" 
          IsChecked=""{Binding AreAllSelected}""/>",
            "Binding Non-Nullable Bool to Tri-State CheckBox",
            "public bool IsSelected { get; set; } <!-- Crashes on null! -->",
            "public bool? IsSelected { get; set; } <!-- Handles true, false, and null -->",
            "Tri-state checkboxes require nullable boolean properties.",
            "What data type should be bound to a CheckBox when IsThreeState is True?",
            new[] { "bool", "bool? (Nullable<bool>)", "int", "string" }, 1,
            "Tri-state checkboxes represent 3 states: true, false, and null (indeterminate)."
        );

        // 73. Toggle and Radio Buttons
        AddTopic(
            "r-m-12", 12, "Toggle & Radio Buttons: Mutually Exclusive Choices",
            "GroupName grouping, custom toggle switch templates, and option routing", 6,
            new[] { "RadioButton selects one option from a mutually exclusive group.", "GroupName groups buttons across different layout panels.", "ToggleButtons provide on/off switch behavior." },
            "<p>Use RadioButtons for unit selections: [Feet] vs [Millimeters] vs [Meters].</p>",
            "Grouped RadioButtons",
            @"<StackPanel Orientation=""Horizontal"">
    <RadioButton Content=""Imperial (Feet)"" GroupName=""Units"" IsChecked=""{Binding IsImperial}""/>
    <RadioButton Content=""Metric (mm)"" GroupName=""Units"" IsChecked=""{Binding IsMetric}"" Margin=""12,0,0,0""/>
</StackPanel>",
            "Omitting GroupName Across Different Containers",
            "<!-- In different StackPanels, they won't uncheck each other! -->",
            "<RadioButton GroupName=\"ExportMode\" ... />",
            "GroupName explicitly binds radio buttons regardless of container nesting.",
            "What property ensures RadioButtons in different containers remain mutually exclusive?",
            new[] { "GroupKey", "GroupName", "RadioSet", "Scope" }, 1,
            "The GroupName attribute identifies which set of radio buttons are mutually exclusive."
        );

        // 74. ItemControl
        AddTopic(
            "r-m-13", 13, "ItemsControl: The Base Collection Repeater",
            "ItemsSource, ItemTemplate, and ItemsPanelTemplate", 7,
            new[] { "ItemsControl is the foundation for all collection controls (ListBox, ComboBox).", "Use ItemsControl when you need to display items without selection behavior.", "ItemTemplate defines how each item renders using DataTemplates." },
            "<p>Ideal for displaying parameter lists, warning badges, or tag chips.</p>",
            "Tag Badge Repeater with ItemsControl",
            @"<ItemsControl ItemsSource=""{Binding ActiveCategories}"">
    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <Border Background=""#162034"" BorderBrush=""#00EBFF"" BorderThickness=""1"" CornerRadius=""4"" Padding=""4,2"" Margin=""2"">
                <TextBlock Text=""{Binding}"" Foreground=""#00EBFF"" FontSize=""11""/>
            </Border>
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>",
            "Using ListBox When Selection is Not Needed",
            "<ListBox ItemsSource=\"{Binding Tags}\"/> <!-- Adds blue selection highlight on click -->",
            "<ItemsControl ItemsSource=\"{Binding Tags}\"/> <!-- Clean display without selection overhead -->",
            "ItemsControl avoids selection logic and click focus rings.",
            "Which control should you use to display a repeated list of items when selection is NOT required?",
            new[] { "ListBox", "ComboBox", "ItemsControl", "ListView" }, 2,
            "ItemsControl displays collections without adding unwanted selection behavior."
        );

        // 75. ListBox
        AddTopic(
            "r-m-14", 14, "ListBox: Single & Multi-Selection Lists",
            "SelectionMode (Single, Multiple, Extended) and SelectedItem binding", 8,
            new[] { "ListBox allows selecting one or multiple items.", "SelectionMode='Extended' supports Shift+Click (range) and Ctrl+Click (multi-select).", "SelectedItems cannot be directly bound in vanilla XAML; handle in ViewModel or code-behind." },
            "<p>The workhorse control for selecting Revit levels, sheets, or views to export.</p>",
            "Multi-Select ListBox",
            @"<ListBox ItemsSource=""{Binding AvailableSheets}"" 
         SelectedItem=""{Binding SelectedSheet}""
         SelectionMode=""Extended"" 
         DisplayMemberPath=""Name"" 
         Height=""220""/>",
            "Two-Way Binding on SelectedItems",
            "<ListBox SelectedItems=\"{Binding ChosenList, Mode=TwoWay}\"/> <!-- COMPILE/RUNTIME ERROR! -->",
            "// SelectedItems is read-only in WPF; use CheckBox inside ItemTemplate instead!",
            "WPF's SelectedItems collection does not support two-way dependency property binding.",
            "What SelectionMode enables standard Windows Ctrl+Click and Shift+Click behavior?",
            new[] { "Single", "Multiple", "Extended", "Full" }, 2,
            "SelectionMode.Extended enables standard multi-selection using Ctrl and Shift keys."
        );

        // 76. ComboBox
        AddTopic(
            "r-m-15", 15, "ComboBox: Dropdown Selection Controls",
            "IsEditable, SelectedValue, SelectedValuePath, and DisplayMemberPath", 7,
            new[] { "ComboBox provides compact dropdown selection.", "DisplayMemberPath specifies which property to show in the dropdown.", "IsEditable = true enables text auto-complete filtering." },
            "<p>Used in almost every tool to let users pick a Level or FamilyType.</p>",
            "Level Picker ComboBox",
            @"<ComboBox ItemsSource=""{Binding LevelItems}""
          SelectedItem=""{Binding SelectedLevel}""
          DisplayMemberPath=""Name""
          SelectedIndex=""0""/>",
            "Binding Full Object vs ID",
            "<ComboBox SelectedValuePath=\"Id\" SelectedItem=\"{Binding LevelId}\"/> <!-- Type mismatch! -->",
            "<ComboBox SelectedItem=\"{Binding SelectedLevel}\"/> <!-- Binds full object cleanly -->",
            "Keep SelectedItem and property types aligned in your ViewModel.",
            "What ComboBox property specifies which property name to display as text for each item?",
            new[] { "TextPath", "DisplayMemberPath", "ViewProperty", "LabelName" }, 1,
            "DisplayMemberPath specifies the property on each bound item to display in the list."
        );

        // 77. Control Style
        AddTopic(
            "r-m-16", 16, "WPF Control Styles: Reusable Aesthetics",
            "Style, TargetType, BasedOn, and Setters", 8,
            new[] { "Styles centralize visual appearance, like CSS classes for WPF.", "TargetType specifies which control the style applies to.", "BasedOn allows style inheritance, extending existing styles." },
            "<p>Define dark-mode styles for Buttons, TextBoxes, and Borders once, and reuse them across every window in your add-in.</p>",
            "Global Cyberpunk Button Style",
            @"<Style x:Key=""NeonButton"" TargetType=""Button"">
    <Setter Property=""Background"" Value=""#0078D4""/>
    <Setter Property=""Foreground"" Value=""White""/>
    <Setter Property=""FontWeight"" Value=""SemiBold""/>
    <Setter Property=""Padding"" Value=""12,6""/>
</Style>

<!-- Usage: -->
<Button Style=""{StaticResource NeonButton}"" Content=""Run Audit""/>",
            "Copy-Pasting Colors on Every Control",
            "<Button Background=\"#0078D4\" Foreground=\"White\"/><Button Background=\"#0078D4\" Foreground=\"White\"/>",
            "<Button Style=\"{StaticResource PrimaryBtn}\"/>",
            "Inline styles make theme changes difficult and bloat XAML.",
            "What attribute specifies which existing style a new WPF Style inherits from?",
            new[] { "Extends", "Inherits", "BasedOn", "ParentStyle" }, 2,
            "The BasedOn attribute allows a style to inherit all setters from a parent style."
        );

        // 78. Window Resources
        AddTopic(
            "r-m-17", 17, "Window Resources & StaticResource vs DynamicResource",
            "Window.Resources, StaticResource resolution, and DynamicResource runtime changes", 7,
            new[] { "Window.Resources stores styles, brushes, converters, and templates local to that window.", "StaticResource resolves at load time (fastest).", "DynamicResource resolves continuously at runtime (used for live theme switching)." },
            "<p>Converters (e.g. BoolToVisibilityConverter) are declared inside Window.Resources.</p>",
            "Declaring Converters in Window Resources",
            @"<Window.Resources>
    <BooleanToVisibilityConverter x:Key=""BoolToVis""/>
    <SolidColorBrush x:Key=""AccentCyan"" Color=""#00EBFF""/>
</Window.Resources>

<ProgressBar Visibility=""{Binding IsLoading, Converter={StaticResource BoolToVis}}""/>",
            "Using StaticResource for Runtime Theme Changes",
            "Background=\"{StaticResource ThemeBg}\" <!-- Doesn't update when theme toggles! -->",
            "Background=\"{DynamicResource ThemeBg}\" <!-- Updates live when user clicks Dark Mode! -->",
            "DynamicResource is required when resource dictionary values change while the window is open.",
            "Which resource lookup mode should be used when colors change dynamically at runtime (theme toggle)?",
            new[] { "StaticResource", "DynamicResource", "LocalResource", "CompileResource" }, 1,
            "DynamicResource maintains an expression that re-evaluates whenever the dictionary changes."
        );

        // 79. Resource Dictionary
        AddTopic(
            "r-m-18", 18, "Resource Dictionaries: Modular Theming",
            "ResourceDictionary, MergedDictionaries, and sharing themes across add-ins", 8,
            new[] { "ResourceDictionaries store themes and styles in standalone XAML files.", "MergedDictionaries allows combining multiple theme files (Colors.xaml, Controls.xaml).", "Allows entire development teams to share unified BIM brand styles." },
            "<p>Structure your add-in into <code>Themes/Colors.xaml</code> and <code>Themes/Controls.xaml</code>, and merge them in <code>App.xaml</code> or your window root.</p>",
            "Merging Dictionaries in XAML",
            @"<Window.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source=""pack://application:,,,/BimSuite;component/Themes/Colors.xaml""/>
            <ResourceDictionary Source=""pack://application:,,,/BimSuite;component/Themes/Buttons.xaml""/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Window.Resources>",
            "Circular Dictionary References",
            "<!-- Colors.xaml merges Buttons.xaml which merges Colors.xaml -> STACK OVERFLOW! -->",
            "// Ensure a clean unidirectional dependency tree for resource dictionaries.",
            "Circular XAML imports crash the XAML parser.",
            "What XAML collection merges multiple external resource dictionary files together?",
            new[] { "MergedDictionaries", "CombinedResources", "IncludedStyles", "ExternalThemes" }, 0,
            "ResourceDictionary.MergedDictionaries combines multiple dictionary files into one scope."
        );

        // 80. Style Trigger
        AddTopic(
            "r-m-19", 19, "Style Triggers: Interactive UI Reactions",
            "Property triggers (IsMouseOver, IsPressed) and DataTriggers", 8,
            new[] { "Property Triggers change styles automatically when a control property changes (e.g. IsMouseOver).", "DataTriggers change styles based on bound ViewModel properties (e.g. ErrorCount > 0).", "Eliminates imperative C# code for hover effects and highlights." },
            "<p>Create glowing buttons on hover entirely in XAML using Style Triggers.</p>",
            "Hover Glow Trigger",
            @"<Style TargetType=""Button"">
    <Setter Property=""Background"" Value=""#162034""/>
    <Style.Triggers>
        <!-- Hover reaction -->
        <Trigger Property=""IsMouseOver"" Value=""True"">
            <Setter Property=""Background"" Value=""#0078D4""/>
            <Setter Property=""Cursor"" Value=""Hand""/>
        </Trigger>
    </Style.Triggers>
</Style>",
            "Writing C# MouseEnter Events for Hover Colors",
            "btn.MouseEnter += (s, e) => btn.Background = Brushes.Blue; // Clutters code-behind!",
            "<!-- Handle cleanly in XAML Triggers! -->",
            "Style Triggers keep presentation logic in XAML where it belongs.",
            "Which XAML trigger changes a control's style based on a bound property in the ViewModel?",
            new[] { "EventTrigger", "Property Trigger", "DataTrigger", "MultiTrigger" }, 2,
            "DataTriggers evaluate data binding expressions rather than direct UI control properties."
        );

        // 81. Material Design Package
        AddTopic(
            "r-m-20", 20, "Material Design in XAML for Revit Add-ins",
            "MaterialDesignThemes NuGet package, card controls, shadow effects, and icons", 8,
            new[] { "MaterialDesignThemes is the most popular WPF design system on NuGet.", "Gives your Revit add-in stunning modern cards, floating buttons, and ripple animations.", "Requires zero designer effort to achieve Silicon Valley-grade aesthetics." },
            "<p>Install <code>MaterialDesignThemes</code> via NuGet to instantly upgrade your Revit add-in from 1990s Windows Forms to sleek modern Google Material Design.</p>",
            "Material Design Card Example",
            @"<!-- Using MaterialDesign Card Control -->
<materialDesign:Card Padding=""16"" Margin=""8"" Background=""#101726"" UniformCornerRadius=""8"">
    <StackPanel>
        <TextBlock Text=""Wall Area Calculation"" Style=""{StaticResource MaterialDesignHeadline6TextBlock}""/>
        <ProgressBar Value=""75"" Style=""{StaticResource MaterialDesignLinearProgressBar}"" Margin=""0,8""/>
    </StackPanel>
</materialDesign:Card>",
            "Resource Conflict with Revit's Internal Styles",
            "// Missing Pack URI imports causes MaterialDesign resources to throw XamlParseException.",
            "// Ensure MaterialDesign bundle is merged in Window.Resources.",
            "Revit doesn't have an App.xaml by default; dictionaries must be merged in Window scope.",
            "What popular NuGet package brings Google Material Design styles to WPF add-ins?",
            new[] { "MaterialDesignThemes", "MetroWPF", "RevitMaterialUI", "BootstrapWPF" }, 0,
            "MaterialDesignThemes is the standard open-source library implementing Material Design in WPF."
        );

        // 82. MVVM Design Pattern
        AddTopic(
            "r-m-21", 21, "MVVM Design Pattern: The Golden Standard",
            "Model, View, ViewModel decoupling and separation of concerns", 9,
            new[] { "Model: Business entities and raw data (Revit Elements, Wall quantities).", "View: Pure XAML presentation layer.", "ViewModel: Exposes observable data and commands; completely independent of UI controls.", "Enables unit testing without launching Revit." },
            "<p>In MVVM, the View knows about the ViewModel, but the ViewModel knows <strong>NOTHING</strong> about the View. There are no <code>txtBox.Text</code> calls in your ViewModel!</p>",
            "The Three Layers Illustrated",
            @"// 1. Model (Data)
public record WallData(int Id, string Name, double Area);

// 2. ViewModel (Logic & State)
public class WallAuditorViewModel : INotifyPropertyChanged { ... }

// 3. View (XAML)
<Window DataContext=""{Binding WallAuditorViewModel}""> ... </Window>",
            "Manipulating UI Controls Directly in Business Logic",
            "txtStatus.Text = \"Done\"; progressBar.Value = 100; // Tightly coupled!",
            "StatusMessage = \"Done\"; ProgressPercent = 100; // Decoupled ViewModel properties!",
            "Tightly coupled code-behind cannot be automated, tested, or reused.",
            "In MVVM, what component holds presentation state and executes commands without knowing UI control names?",
            new[] { "View", "Model", "ViewModel", "Controller" }, 2,
            "The ViewModel encapsulates data and commands for the view, decoupled from specific controls."
        );

        // 83. MVVM Professional
        AddTopic(
            "r-m-22", 22, "MVVM Professional: RelayCommand & ObservableObject",
            "INotifyPropertyChanged, CallerMemberName, RelayCommand, and AsyncRelayCommand", 9,
            new[] { "ObservableObject implements INotifyPropertyChanged using [CallerMemberName].", "RelayCommand wraps ICommand delegates for button actions.", "CommunityToolkit.Mvvm NuGet provides source generators that eliminate 80% of boilerplate." },
            "<p>Use <code>[CallerMemberName]</code> so you never have to type property names as magic strings in <code>OnPropertyChanged()</code>.</p>",
            "Base ObservableObject & RelayCommand",
            @"public abstract class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public class RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null) : ICommand
{
    public bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;
    public void Execute(object? parameter) => execute(parameter);
    public event EventHandler? CanExecuteChanged { add {} remove {} }
}",
            "Hardcoding Property Names as Strings in PropertyChanged",
            "OnPropertyChanged(\"CommentText\"); // Breaks silently if property is renamed!",
            "OnPropertyChanged(); // Automatically uses [CallerMemberName] safely!",
            "CallerMemberName ensures compile-safe property notifications.",
            "What C# attribute automatically captures the calling property name in OnPropertyChanged?",
            new[] { "[CallerMemberName]", "[PropertyName]", "[Observable]", "[Notify]" }, 0,
            "[CallerMemberName] from System.Runtime.CompilerServices injects the property name automatically."
        );

        // 84. Binding Controls
        AddTopic(
            "r-m-23", 23, "Binding Controls: Two-Way Data Binding in Action",
            "Mode=TwoWay, Mode=OneWay, StringFormat, and Converters", 8,
            new[] { "Mode=TwoWay: Changes in UI update ViewModel; changes in ViewModel update UI.", "Mode=OneWay: Read-only display from ViewModel to UI.", "StringFormat allows inline formatting in XAML (StringFormat='{}{0:F2} mm')." },
            "<p>Two-way binding synchronizes user inputs directly with your C# properties without any event listeners.</p>",
            "Formatted Two-Way Binding in XAML",
            @"<!-- Two-way text binding with culture formatting -->
<TextBox Text=""{Binding TargetOffset, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"" Margin=""4""/>

<!-- One-way formatted readout -->
<TextBlock Text=""{Binding TotalArea, StringFormat='Total Area: {0:F2} sq.ft'}"" Foreground=""#00EBFF""/>",
            "Forgetting Mode=TwoWay on Custom Controls",
            "<MySlider Value=\"{Binding ZoomLevel}\"/> <!-- Defaults to OneWay, doesn't update VM! -->",
            "<MySlider Value=\"{Binding ZoomLevel, Mode=TwoWay}\"/>",
            "Some properties default to OneWay binding unless specified otherwise.",
            "What binding mode should be used for input fields where user typing must update the ViewModel?",
            new[] { "OneTime", "OneWay", "TwoWay", "OneWayToSource" }, 2,
            "Mode=TwoWay synchronizes data bi-directionally between UI and ViewModel."
        );

        // 85-88: Guided Project 1 - Model Navigator (Parts 01 to 04)
        AddTopic(
            "r-m-24", 24, "Example Model Navigator 01: Project Architecture",
            "Building an interactive 3D model navigation tool in WPF + Revit API: Architecture & Specs", 11,
            new[] { "Project Goal: Build a dockable/modal tool that categories and lists all model elements.", "Architecture: ModelNavigatorView (WPF) + ModelNavigatorViewModel (MVVM) + NavigationService (Revit API).", "Allows clicking an element in the list to immediately zoom and highlight it in Revit!" },
            "<p>In Part 1 of this master project, we design the solution architecture, set up the data models, and establish the communication contract between Revit's <code>UIDocument.ShowElements()</code> and our WPF list.</p>",
            "Model Navigator Data Model",
            @"public record NavigatorElementItem(
    int ElementId, 
    string Category, 
    string FamilyName, 
    string TypeName, 
    string Mark);",
            "Mixing Revit Database Logic Inside the WPF ViewModel",
            "// Directly calling new FilteredElementCollector inside ViewModel",
            "// Decouple: pass an INavigationService into ViewModel",
            "Decoupled services allow testing UI without Revit running.",
            "What Revit API method centers the viewport camera on specified elements?",
            new[] { "uidoc.ShowElements()", "uidoc.Zoom()", "doc.Focus()", "activeView.Center()" }, 0,
            "UIDocument.ShowElements() centers and zooms the active view onto the given ElementIds."
        );

        AddTopic(
            "r-m-25", 25, "Example Model Navigator 02: ViewModel & Selection Sync",
            "Model Navigator Part 2: Observable Collections and Zooming Viewport on SelectedItem", 11,
            new[] { "ObservableCollection<T> automatically updates WPF ListView when items are added/removed.", "When SelectedItem changes, trigger NavigationService.ZoomToElement(id).", "Uses uidoc.Selection.SetElementIds() to highlight selected components." },
            "<p>In Part 2, we implement the <code>ObservableCollection&lt;NavigatorElementItem&gt;</code> and wire the selection change event to Revit's graphics viewport.</p>",
            "Model Navigator ViewModel Selection Handler",
            @"public class ModelNavigatorViewModel : ObservableObject
{
    private NavigatorElementItem? _selectedItem;
    public NavigatorElementItem? SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged();
            if (_selectedItem != null)
            {
                // Zoom Revit camera to this element:
                _navService.ZoomTo(_selectedItem.ElementId);
            }
        }
    }
}",
            "Calling ShowElements from Background Thread",
            "Task.Run(() => uidoc.ShowElements(id)); // CRASH: Thread affinity violation!",
            "_navService.ZoomTo(id); // Main thread execution",
            "Viewport zooming must execute strictly on Revit's main UI thread.",
            "What collection type in WPF automatically notifies the UI when items are added or deleted?",
            new[] { "List<T>", "ObservableCollection<T>", "HashSet<T>", "Queue<T>" }, 1,
            "ObservableCollection<T> implements INotifyCollectionChanged, notifying the UI of additions and removals."
        );

        AddTopic(
            "r-m-26", 26, "Example Model Navigator 03: Live Category Tree & Search",
            "Model Navigator Part 3: Instant search filtering and Category grouping", 11,
            new[] { "CollectionViewSource supports live filtering without re-querying the Revit database.", "Instant search as the user types element mark or family name.", "Category tree grouping with Expander controls in XAML." },
            "<p>In Part 3, we add a high-speed search bar that filters 5,000 model elements in real-time using an ICollectionView filter predicate.</p>",
            "Live CollectionView Filter Predicate",
            @"private ICollectionView _view;

public ModelNavigatorViewModel()
{
    _view = CollectionViewSource.GetDefaultView(Elements);
    _view.Filter = FilterElements;
}

private bool FilterElements(object item)
{
    if (string.IsNullOrWhiteSpace(SearchText)) return true;
    var elem = (NavigatorElementItem)item;
    return elem.FamilyName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
           elem.Mark.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
}",
            "Re-querying FilteredElementCollector on Every Keystroke",
            "// Querying Revit database on every letter typed -> 2-second lag per key!",
            "// Query once into memory, then filter the in-memory ICollectionView!",
            "In-memory filtering delivers 60fps instant search responsiveness.",
            "What WPF interface provides live in-memory filtering, sorting, and grouping for lists?",
            new[] { "ICollectionView", "IDataFilter", "ILiveList", "IQueryable" }, 0,
            "ICollectionView provides sorting, filtering, and grouping over an underlying collection."
        );

        AddTopic(
            "r-m-27", 27, "Example Model Navigator 04: Production Polish & Modeless Mode",
            "Model Navigator Part 4: Modeless ExternalEvent integration and release packaging", 12,
            new[] { "Convert to a floating modeless palette so users can navigate while editing the building.", "Wire ExternalEvent to safely select elements without modal dialog lockup.", "Add copy-to-clipboard button for ElementIds." },
            "<p>In the final part of Model Navigator, we convert the window to modeless execution with an <code>IExternalEventHandler</code>, polish the Cyberpunk dark theme, and compile the production build.</p>",
            "Complete ExternalEvent Handler for Navigator",
            @"public class NavigateSelectionHandler : IExternalEventHandler
{
    public int TargetElementId { get; set; }

    public void Execute(UIApplication uiapp)
    {
        var uidoc = uiapp.ActiveUIDocument;
        var id = new ElementId(TargetElementId);
        uidoc.Selection.SetElementIds(new List<ElementId> { id });
        uidoc.ShowElements(id);
    }

    public string GetName() => ""ModelNavigatorSync"";
}",
            "Calling Document.Close while Navigator is Open",
            "// Navigator holds old Document reference after user closes project -> Crash!",
            "// Subscribe to Application.ViewActivated to update active document handle!",
            "Modeless tools must track active document changes gracefully.",
            "Why is ExternalEvent needed for a modeless Model Navigator to select elements in Revit?",
            new[] { "To avoid thread collision and acquire Revit's main API context", "To encrypt the ElementId", "To speed up Windows", "To connect to internet" }, 0,
            "ExternalEvent marshals requests from the modeless UI onto Revit's main thread with valid API context."
        );

        // 89-92: Guided Project 2 - Room Wizard (Parts 01 to 04)
        AddTopic(
            "r-m-28", 28, "Room Wizard 01: Automated Creation Architecture",
            "Automated Room Creation, Tagging, and Parameter Tool: Architecture & Room API", 12,
            new[] { "Project Goal: Build an enterprise batch Room creation and tagging automation tool.", "Revit API: doc.Create.NewRoom(Level level, UV point) and NewRoomTag.", "Detects unplaced rooms, calculates boundary areas, and bulk-applies department codes." },
            "<p>In Part 1 of Room Wizard, we explore the Room creation API, understand plan topology circuits (PlanTopology / PlanCircuit), and outline the data architecture.</p>",
            "Creating a Room from PlanCircuit in C#",
            @"// Create room within closed wall boundary circuit:
PlanCircuit circuit = GetSelectedCircuit();
using (Transaction trans = new Transaction(doc, ""Create Room""))
{
    trans.Start();
    Room newRoom = doc.Create.NewRoom(circuit);
    newRoom.Name = ""Conference Room"";
    newRoom.Number = ""101"";
    trans.Commit();
}",
            "Placing Rooms in Unenclosed Boundary Loops",
            "doc.Create.NewRoom(level, point); // Creates unplaced/redundant room if boundary isn't closed!",
            "// Verify circuit.IsRoomLocated == false before creating room!",
            "Rooms require continuous closed bounding walls or room separation lines.",
            "What class represents a closed spatial boundary circuit for creating Rooms in Revit API?",
            new[] { "PlanCircuit", "SpatialLoop", "BoundaryPolygon", "RoomAreaLoop" }, 0,
            "PlanCircuit represents a 2D closed polygon of wall/separation lines capable of hosting a Room."
        );

        AddTopic(
            "r-m-29", 29, "Room Wizard 02: WPF Interface & Department Picker",
            "Room Wizard Part 2: Building the Material Design configuration wizard in XAML", 11,
            new[] { "Build a step-by-step wizard UI with Material Design styling.", "Include Department picker, room numbering prefix generator, and floor level selector.", "Input validation: ensures room numbering format complies with BIM standards." },
            "<p>In Part 2, we build the interactive wizard UI with validation rules for room numbering formats (e.g. 'LEVEL-DEPT-NUM').</p>",
            "Room Wizard XAML Layout Snippet",
            @"<materialDesign:Card Padding=""20"" Margin=""10"">
    <StackPanel>
        <TextBlock Text=""Room Generation Wizard"" FontSize=""18"" FontWeight=""Bold""/>
        <TextBox Text=""{Binding NumberPrefix}"" materialDesign:HintAssist.Hint=""Prefix (e.g. CONF-)"" Margin=""0,12""/>
        <ComboBox ItemsSource=""{Binding Departments}"" SelectedItem=""{Binding SelectedDept}"" Margin=""0,8""/>
        <Button Content=""Generate Rooms"" Command=""{Binding GenerateCommand}"" Style=""{StaticResource MaterialDesignRaisedButton}"" Margin=""0,16,0,0""/>
    </StackPanel>
</materialDesign:Card>",
            "Binding Without Validation Error Notifications",
            "// Missing IDataErrorInfo allows user to submit empty prefix -> corrupts numbering!",
            "// Implement INotifyDataErrorInfo to highlight invalid fields in red!",
            "Validating inputs prevents creating rooms with duplicate or empty room numbers.",
            "What Material Design assist property provides floating hint text in modern textboxes?",
            new[] { "HintAssist.Hint", "TextBox.Placeholder", "Watermark.Text", "InputHint" }, 0,
            "materialDesign:HintAssist.Hint provides floating label/placeholder text in MaterialDesignInXAML."
        );

        AddTopic(
            "r-m-30", 30, "Room Wizard 03: Boundary Math & Room Tags",
            "Room Wizard Part 3: Placing Room Tags at geometric centroids & calculating net area", 12,
            new[] { "doc.Create.NewRoomTag(LinkElementId, UV point, ViewId).", "Calculate room geometric centroid from ClosedShell faces for perfect tag placement.", "Automate room parameter population (Occupancy, Department, Finishes)." },
            "<p>In Part 3, we calculate the room's geometric center and place an automated RoomTag hosted directly in the active plan view.</p>",
            "Placing Room Tag at Centroid",
            @"using (Transaction trans = new Transaction(doc, ""Tag Room""))
{
    trans.Start();

    // 1. Get room center point:
    LocationPoint loc = (LocationPoint)room.Location;
    UV tagPoint = new UV(loc.Point.X, loc.Point.Y);

    // 2. Place RoomTag:
    RoomTag tag = doc.Create.NewRoomTag(new LinkElementId(room.Id), tagPoint, doc.ActiveView.Id);

    trans.Commit();
}",
                    "Placing Room Tags in 3D Views",
            "doc.Create.NewRoomTag(linkId, pt, view3d.Id); // CRASH: Room tags require Plan or Section view!",
            "if (doc.ActiveView is ViewPlan planView) { /* Safe */ }",
            "Room tags can only be created in 2D views (Floor Plans, Ceiling Plans, Sections).",
            "What view types permit the creation of native Revit RoomTags?",
            new[] { "3D Views only", "2D Plan and Section views only", "Schedules only", "Rendering views" }, 1,
            "Room tags are 2D annotation symbols permitted only in 2D plan, ceiling, and section views."
        );

        AddTopic(
            "r-m-31", 31, "Room Wizard 04: Batch Execution & Quantity Schedules",
            "Room Wizard Part 4: Transaction Groups, FailuresPreprocessor, and Summary Reports", 12,
            new[] { "Batch execution: Create 100 rooms in seconds using a single TransactionGroup.", "Suppress duplicate room number warnings using custom IFailuresPreprocessor.", "Output an automated executive summary report showing total net sq.meters." },
            "<p>In the grand finale of the academy, we wrap our Room Wizard in a <code>TransactionGroup</code> with an <code>IFailuresPreprocessor</code> that swallows harmless warnings, and generate a final quantity schedule.</p>",
            "Batch Room Creation with Failure Preprocessor",
            @"public class SilentWarningSwallower : IFailuresPreprocessor
{
    public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
    {
        failuresAccessor.DeleteAllWarnings();
        return FailureProcessingResult.Continue;
    }
}

// In Room Wizard Command:
using (var group = new TransactionGroup(doc, ""Batch Room Wizard""))
{
    group.Start();

    using (var trans = new Transaction(doc, ""Generate All Rooms""))
    {
        var options = trans.GetFailureHandlingOptions();
        options.SetFailuresPreprocessor(new SilentWarningSwallower());
        trans.SetFailureHandlingOptions(options);

        trans.Start();
        // Batch generate 100 rooms & tags...
        trans.Commit();
    }

    group.Assimilate(); // Single Ctrl+Z undo item!
}",
            "Allowing Duplicate Number Popups to Halt Batch Execution",
            "// Revit pops up 100 warning dialogs for duplicate marks, requiring 100 clicks!",
            "// Use IFailuresPreprocessor to suppress duplicate warnings automatically!",
            "Failure preprocessors ensure automated tools run unattended without blocking.",
            "What interface allows a Revit add-in to automatically resolve and dismiss warning popups?",
            new[] { "IFailuresPreprocessor", "IWarningHandler", "IPopupSuppressor", "IErrorHandler" }, 0,
            "IFailuresPreprocessor intercepts failures and warnings during transaction commit, allowing silent resolution."
        );

        return mod;
    }
}
