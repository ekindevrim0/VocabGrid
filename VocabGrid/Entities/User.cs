using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

    /// <summary>Profil görseli URL; yoksa UI initials kullanır.</summary>
    public string? AvatarUrl { get; set; }

    public string NativeLanguage { get; set; } = "English";
    public string TargetLanguage { get; set; } = "Turkish";

    /// <summary>Figma: Intermediate / Beginner / Advanced vb.</summary>
    public string TargetProficiencyLevel { get; set; } = "Beginner";

    public int DailyGoalMinutes { get; set; } = 10;
    public int CurrentStreak { get; set; } = 0;
    public int LongestStreak { get; set; } = 0;
    public int Level { get; set; } = 1;
    public int TotalXp { get; set; } = 0;

    /// <summary>Figma: Upgrade to Premium / PRO</summary>
    public bool IsPremium { get; set; }

    public string? GoogleId { get; set; }
    public string? AppleId { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public UserSettings? Settings { get; set; }
    public ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();
    public ICollection<UserLearningPurpose> UserLearningPurposes { get; set; } = new List<UserLearningPurpose>();
    public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
    public ICollection<Deck> Decks { get; set; } = new List<Deck>();
    public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
    public ICollection<UserWordProgress> UserWordProgresses { get; set; } = new List<UserWordProgress>();
    public ICollection<StudyActivity> StudyActivities { get; set; } = new List<StudyActivity>();
    public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();
    public ICollection<QuizSession> QuizSessions { get; set; } = new List<QuizSession>();
}
