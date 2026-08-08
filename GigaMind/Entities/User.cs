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

    public string NativeLanguage { get; set; } = "English";
    public string TargetLanguage { get; set; } = "Turkish";
    public int DailyGoalMinutes { get; set; } = 10;
    public int CurrentStreak { get; set; } = 0;
    public int Level { get; set; } = 1;

    public ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();
}