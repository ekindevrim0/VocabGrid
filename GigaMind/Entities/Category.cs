namespace VocabGrid.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsSelected { get; set; }

    public ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();
}