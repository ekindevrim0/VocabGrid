namespace VocabGrid.DTOs;

public class UserProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string NativeLanguage { get; set; } = string.Empty;
    public string TargetLanguage { get; set; } = string.Empty;
    public string TargetProficiencyLevel { get; set; } = string.Empty;
    public int DailyGoalMinutes { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public int Level { get; set; }
    public int TotalXp { get; set; }
    public bool IsPremium { get; set; }
}

public class UpdateUserProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? NativeLanguage { get; set; }
    public string? TargetLanguage { get; set; }
    public string? TargetProficiencyLevel { get; set; }
    public int DailyGoalMinutes { get; set; }
}

public class UserSettingsDto
{
    public bool DarkMode { get; set; }
    public bool DailyReminders { get; set; }
    public bool SoundEffects { get; set; }
    public string ThemeColor { get; set; } = "Purple";
    public string TextSize { get; set; } = "Medium";
    public string DifficultyMode { get; set; } = "Adaptive";
}
