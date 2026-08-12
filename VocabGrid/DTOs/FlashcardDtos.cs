using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public class CreateFlashcardDto
{
    [Required]
    public int DeckId { get; set; }

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

public class UpdateFlashcardDto
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
