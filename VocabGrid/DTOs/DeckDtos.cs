using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public class CreateDeckDto
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// Yalnızca uygulamanın oluşturduğu başlangıç desteleri doldurur
    /// ("basics_DE"). Kullanıcının kendi destesinde boş kalır.
    /// </summary>
    [MaxLength(40)]
    public string? StarterKey { get; set; }
}

public class UpdateDeckDto
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }
}
