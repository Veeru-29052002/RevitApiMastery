namespace RevitApiMastery.Services;

public class UserProgressState
{
    public HashSet<string> CompletedTopicIds { get; set; } = new();
    public HashSet<string> BookmarkedTopicIds { get; set; } = new();
    public Dictionary<string, int> QuizScores { get; set; } = new();
    public string ActiveTrackId { get; set; } = "csharp";
}

public class ProgressService
{
    public UserProgressState State { get; private set; } = new();

    public event Action? OnChange;

    public bool IsTopicCompleted(string topicId) => State.CompletedTopicIds.Contains(topicId);

    public void ToggleTopicCompleted(string topicId)
    {
        if (State.CompletedTopicIds.Contains(topicId))
            State.CompletedTopicIds.Remove(topicId);
        else
            State.CompletedTopicIds.Add(topicId);

        NotifyStateChanged();
    }

    public bool IsTopicBookmarked(string topicId) => State.BookmarkedTopicIds.Contains(topicId);

    public void ToggleBookmark(string topicId)
    {
        if (State.BookmarkedTopicIds.Contains(topicId))
            State.BookmarkedTopicIds.Remove(topicId);
        else
            State.BookmarkedTopicIds.Add(topicId);

        NotifyStateChanged();
    }

    public void RecordQuizScore(string topicId, int score)
    {
        State.QuizScores[topicId] = score;
        if (score >= 80)
        {
            State.CompletedTopicIds.Add(topicId);
        }
        NotifyStateChanged();
    }

    public int GetQuizScore(string topicId)
    {
        return State.QuizScores.TryGetValue(topicId, out var score) ? score : -1;
    }

    public void SetActiveTrack(string trackId)
    {
        State.ActiveTrackId = trackId;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
