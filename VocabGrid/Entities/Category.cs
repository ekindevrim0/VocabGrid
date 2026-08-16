using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

/// <summary>
/// Çalışma kategorisi tanımı. Kullanıcı seçimi UserCategory join tablosundadır.
/// </summary>
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    /// <summary>Material icon name for Flutter (e.g. restaurant, flight).</summary>
    [MaxLength(64)]
    public string IconName { get; set; } = string.Empty;

    /// <summary>Accent color hex for Flutter (e.g. #F97316).</summary>
    [MaxLength(16)]
    public string ColorHex { get; set; } = string.Empty;

    public ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();
}
