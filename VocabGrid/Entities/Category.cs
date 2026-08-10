namespace VocabGrid.Entities;

/// <summary>
/// Çalışma kategorisi tanımı. Kullanıcı seçimi UserCategory join tablosundadır.
/// </summary>
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();
}
