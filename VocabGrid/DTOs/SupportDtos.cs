using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public class ReportProblemDto
{
    [Required]
    [MaxLength(4000)]
    public string Message { get; set; } = string.Empty;
}
