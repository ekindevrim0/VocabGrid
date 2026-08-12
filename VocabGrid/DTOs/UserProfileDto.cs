namespace VocabGrid.DTOs
{
    public class UserProfileDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? NativeLanguage { get; set; }
        public string? TargetLanguage { get; set; }
        public int DailyGoalMinutes { get; set; }
    }

    public class UpdateUserProfileDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? NativeLanguage { get; set; }
        public string? TargetLanguage { get; set; }
        public int DailyGoalMinutes { get; set; }
    }

    public class UserSettingsDto
    {
        public bool DarkMode { get; set; }
        public bool DailyReminders { get; set; }
        public bool SoundEffects { get; set; }
        public string TextSize { get; set; } = "Medium";
        public string DifficultyMode { get; set; } = "Medium";
    }
}