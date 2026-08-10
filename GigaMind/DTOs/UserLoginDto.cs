using System.ComponentModel.DataAnnotations;

namespace FlashcardApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string SourceLanguage { get; set; } = "English";

        public string TargetLanguage { get; set; } = "English";

        public int DailyGoalXP { get; set; } = 20;

        public int TotalXP { get; set; } = 0;

        public int CurrentStreak { get; set; } = 0;

        // User Settings
        public string FontSizePreference { get; set; } = "Medium";

        public bool EnableAudio { get; set; } = true;

        // Auth & Tokens
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserProgress>? ProgressRecords { get; set; }
    }
}
