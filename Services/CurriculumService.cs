using RevitApiMastery.Models;

namespace RevitApiMastery.Services;

public partial class CurriculumService
{
    private readonly List<LearningTrack> _tracks = new();
    private readonly List<Module> _allSyllabusModules = new();

    public CurriculumService()
    {
        InitializeSyllabus();
    }

    private void InitializeSyllabus()
    {
        var modCsLvl1 = BuildCSharpLevel1Module();
        var modCsAdv = BuildCSharpAdvancedModule();
        var modRevitLvl1 = BuildRevitApiLevel01Module();
        var modRevitMaster = BuildRevitApiMasterModule();

        BuildDualTrackCurriculum(
            modCsLvl1,
            modCsAdv,
            modRevitLvl1,
            modRevitMaster,
            out var csharpModules,
            out var revitModules);

        _allSyllabusModules.AddRange(csharpModules);
        _allSyllabusModules.AddRange(revitModules);

        // Track 1: C# Programming (Modules C1 to C10)
        var csTrack = new LearningTrack
        {
            Id = "csharp",
            Type = TrackType.CSharpFundamentals,
            Title = "C# Programming Track",
            Subtitle = "Complete 10-Module industrial C# curriculum from scratch to architecture",
            Description = "Master C# from syntax, variables, and memory to OOP, LINQ, Async/Await, Pattern Matching, and Design Patterns.",
            Icon = "bi-filetype-cs",
            AccentColor = "#A855F7",
            Modules = csharpModules
        };

        // Track 2: Revit API C# (Modules R1 to R10)
        var revitTrack = new LearningTrack
        {
            Id = "revitapi",
            Type = TrackType.RevitApi,
            Title = "Revit API C# Track",
            Subtitle = "Complete 10-Module professional Revit API mastery",
            Description = "Master the Revit API from DB architecture and transactions to collectors, parameters, 3D geometry, family creation, WPF MVVM, and DMU.",
            Icon = "bi-boxes",
            AccentColor = "#00EBFF",
            Modules = revitModules
        };

        _tracks.Add(csTrack);
        _tracks.Add(revitTrack);
    }

    public IReadOnlyList<LearningTrack> GetTracks() => _tracks;

    public IReadOnlyList<Module> GetAllSyllabusModules() => _allSyllabusModules;

    public LearningTrack? GetTrack(string trackId) =>
        _tracks.FirstOrDefault(t => string.Equals(t.Id, trackId, StringComparison.OrdinalIgnoreCase));

    public Module? GetModule(string moduleId)
    {
        if (string.IsNullOrWhiteSpace(moduleId)) return null;

        // Exact match on Id or ModuleCode (e.g. "C1", "R1", "c-mod-1")
        var direct = _allSyllabusModules.FirstOrDefault(m => 
            string.Equals(m.Id, moduleId, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(m.ModuleCode, moduleId, StringComparison.OrdinalIgnoreCase));
        if (direct != null) return direct;

        // Legacy module ID mappings
        return moduleId.ToLowerInvariant() switch
        {
            "c-lvl1" => _allSyllabusModules.FirstOrDefault(m => m.ModuleCode == "C1"),
            "c-adv" => _allSyllabusModules.FirstOrDefault(m => m.ModuleCode == "C3"),
            "revit-lvl1" or "revit-lvl01" => _allSyllabusModules.FirstOrDefault(m => m.ModuleCode == "R1"),
            "revit-master" => _allSyllabusModules.FirstOrDefault(m => m.ModuleCode == "R9"),
            _ => null
        };
    }

    public Topic? GetTopic(string topicId)
    {
        if (string.IsNullOrWhiteSpace(topicId)) return null;

        var direct = _allSyllabusModules.SelectMany(m => m.Topics).FirstOrDefault(top => string.Equals(top.Id, topicId, StringComparison.OrdinalIgnoreCase));
        if (direct != null) return direct;

        return topicId.ToLowerInvariant() switch
        {
            "cs1-intro" or "cs-intro" => _allSyllabusModules.SelectMany(m => m.Topics).FirstOrDefault(t => t.Id == "c-l1-01"),
            "revit01-solution" or "revit-intro" => _allSyllabusModules.SelectMany(m => m.Topics).FirstOrDefault(t => t.Id == "r-l1-01"),
            _ => null
        };
    }

    public (Topic? Previous, Topic? Next) GetAdjacentTopics(string topicId)
    {
        var allTopics = _allSyllabusModules.SelectMany(m => m.Topics).ToList();
        var index = allTopics.FindIndex(t => string.Equals(t.Id, topicId, StringComparison.OrdinalIgnoreCase));
        if (index == -1) return (null, null);

        var prev = index > 0 ? allTopics[index - 1] : null;
        var next = index < allTopics.Count - 1 ? allTopics[index + 1] : null;
        return (prev, next);
    }

    public List<Topic> SearchTopics(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Topic>();

        var q = query.Trim().ToLowerInvariant();
        return _allSyllabusModules
            .SelectMany(m => m.Topics)
            .Where(t => t.Title.ToLowerInvariant().Contains(q) ||
                        t.Subtitle.ToLowerInvariant().Contains(q) ||
                        t.KeyTakeaways.Any(k => k.ToLowerInvariant().Contains(q)) ||
                        t.CodeSnippets.Any(c => c.Code.ToLowerInvariant().Contains(q) || c.Title.ToLowerInvariant().Contains(q)))
            .Take(15)
            .ToList();
    }

    public List<CurriculumTier> GetTrackTiers(string trackId)
    {
        var track = GetTrack(trackId);
        if (track == null) return new();

        var tiers = new List<CurriculumTier>();
        foreach (var mod in track.Modules)
        {
            tiers.Add(new CurriculumTier
            {
                LevelNumber = mod.LevelNumber,
                Title = mod.LevelTitle,
                Subtitle = mod.ShortDescription,
                Icon = mod.Icon,
                BadgeClass = mod.Level switch
                {
                    DifficultyLevel.Beginner => "badge-beginner",
                    DifficultyLevel.Intermediate => "badge-intermediate",
                    DifficultyLevel.Advanced => "badge-advanced",
                    _ => "badge-expert"
                },
                AccentColor = mod.Level switch
                {
                    DifficultyLevel.Beginner => "#10B981",
                    DifficultyLevel.Intermediate => "#00EBFF",
                    DifficultyLevel.Advanced => "#A855F7",
                    _ => "#F59E0B"
                },
                Modules = new List<Module> { mod }
            });
        }
        return tiers;
    }

    public int GetTotalTopicsCount() =>
        _allSyllabusModules.SelectMany(m => m.Topics).Count();

    public int GetTrackTopicsCount(string trackId) =>
        GetTrack(trackId)?.Modules.SelectMany(m => m.Topics).Count() ?? 0;
}
