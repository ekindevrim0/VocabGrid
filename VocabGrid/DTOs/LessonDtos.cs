using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public class CreateLessonDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(20)]
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

    [MaxLength(20)]
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

    public string? ImageUrl { get; set; }
    public string? AudioUrl { get; set; }
}
