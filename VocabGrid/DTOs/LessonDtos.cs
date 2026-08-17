using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public class CreateLessonDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    // CEFR seviyeleri. Aynı kural veritabanında da CHECK olarak duruyor; buradaki
    // amaç kullanıcıya anlaşılır bir 400 döndürmek, oradaki ise bu yoldan
    // geçmeyen yazmaları da yakalamak.
    [MaxLength(20)]
    [RegularExpression("^(A1|A2|B1|B2|C1|C2)$", ErrorMessage = "Level must be one of A1, A2, B1, B2, C1, C2.")]
    public string Level { get; set; } = "A1";

    public int OrderIndex { get; set; }
}

public class UpdateLessonDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    // CEFR seviyeleri. Aynı kural veritabanında da CHECK olarak duruyor; buradaki
    // amaç kullanıcıya anlaşılır bir 400 döndürmek, oradaki ise bu yoldan
    // geçmeyen yazmaları da yakalamak.
    [MaxLength(20)]
    [RegularExpression("^(A1|A2|B1|B2|C1|C2)$", ErrorMessage = "Level must be one of A1, A2, B1, B2, C1, C2.")]
    public string Level { get; set; } = "A1";

    public int OrderIndex { get; set; }
}

public class AttachLessonWordDto
{
    [Required]
    [MaxLength(200)]
    public string Term { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Translation { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ExampleSentence { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    [MaxLength(500)]
    public string? AudioUrl { get; set; }
}
