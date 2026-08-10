namespace VocabGrid.Entities;

/// <summary>
/// Kullanıcıya özel UI / öğrenme tercihleri (User ile 1:1).
/// Learning purpose çoklu seçim UserLearningPurpose tablosundadır.
/// </summary>
public class UserSettings
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public bool DarkMode { get; set; } = false;
    public bool DailyReminders { get; set; } = true;
    public bool SoundEffects { get; set; } = true;

    /// <summary>Purple, Blue, Green, Orange</summary>
    public string ThemeColor { get; set; } = "Purple";

    /// <summary>Small, Medium, Large, ExtraLarge, Automatic</summary>
    public string TextSize { get; set; } = "Medium";

    /// <summary>Adaptive, Easy, Normal, Hard</summary>
    public string DifficultyMode { get; set; } = "Adaptive";
}
