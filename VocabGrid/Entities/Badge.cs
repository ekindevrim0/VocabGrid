namespace VocabGrid.Entities;

/// <summary>
/// Rozet / achievement tanımı (şablon). Kullanıcı unlock durumu UserBadge'dedir.
/// </summary>
public class Badge
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;

    /// <summary>StreakDays, PerfectQuiz, WordsLearned, CardsPerMinutes, LanguagesStarted</summary>
    public string UnlockCondition { get; set; } = string.Empty;

    /// <summary>Koşul eşiği (örn. 7 gün, 100 kelime).</summary>
    public int Threshold { get; set; }

    public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
}
