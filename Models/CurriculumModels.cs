namespace RevitApiMastery.Models;

public enum TrackType
{
    CSharpFundamentals,
    RevitApi
}

public enum DifficultyLevel
{
    Beginner,
    Intermediate,
    Advanced,
    Expert
}

public class CurriculumTier
{
    public int StageNumber { get; set; } = 1;
    public int LevelNumber { get => StageNumber; set => StageNumber = value; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = "bi-mortarboard";
    public string BadgeClass { get; set; } = "badge-beginner";
    public string AccentColor { get; set; } = "#10B981";
    public List<Module> Modules { get; set; } = new();
}

public class LearningTrack
{
    public string Id { get; set; } = string.Empty;
    public TrackType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string AccentColor { get; set; } = "#00EBFF";
    public List<Module> Modules { get; set; } = new();
}

public class Module
{
    public string Id { get; set; } = string.Empty;
    public string ModuleCode { get; set; } = string.Empty; // e.g. "C1", "C2", "R1", etc.
    public string TrackId { get; set; } = string.Empty;
    public int Order { get; set; }
    public int StageNumber { get; set; } = 1; // 1 = Stage 1 (Basics), 2 = Stage 2 (Intermediate), 3 = Stage 3 (Advanced), 4 = Stage 4 (Expert)
    public int LevelNumber { get => StageNumber; set => StageNumber = value; }
    public string StageTitle { get; set; } = "Stage 1: Basics";
    public string LevelTitle { get => StageTitle; set => StageTitle = value; }
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Icon { get; set; } = "bi-box";
    public DifficultyLevel Level { get; set; }
    public List<Topic> Topics { get; set; } = new();
}

public class Topic
{
    public string Id { get; set; } = string.Empty;
    public string ModuleId { get; set; } = string.Empty;
    public string TrackId { get; set; } = string.Empty;
    public int Order { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public int ReadTimeMinutes { get; set; }
    public DifficultyLevel Level { get; set; }
    public List<string> KeyTakeaways { get; set; } = new();
    public List<TopicSection> Sections { get; set; } = new();
    public List<CodeSnippet> CodeSnippets { get; set; } = new();
    public List<Pitfall> Pitfalls { get; set; } = new();
    public List<QuizQuestion> Quizzes { get; set; } = new();
}

public class TopicSection
{
    public string Title { get; set; } = string.Empty;
    public string ContentHtml { get; set; } = string.Empty;
    public string? CalloutText { get; set; }
    public string? CalloutType { get; set; } // "tip", "warning", "info"
}

public class CodeSnippet
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Language { get; set; } = "csharp";
    public string Code { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
    public string? VersionNote { get; set; }
}

public class Pitfall
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BadCode { get; set; } = string.Empty;
    public string GoodCode { get; set; } = string.Empty;
    public string WhyItMatters { get; set; } = string.Empty;
}

public class QuizQuestion
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Question { get; set; } = string.Empty;
    public string? CodeSnippet { get; set; }
    public List<string> Options { get; set; } = new();
    public int CorrectOptionIndex { get; set; }
    public string Explanation { get; set; } = string.Empty;
}
