using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public sealed class UpdateLessonProgressDto
{
    [Range(0, 100)]
    public int Score { get; init; }

    public bool Completed { get; init; }

    [Range(0, 86400)]
    public int StudyDurationSeconds { get; init; }
}
public sealed class SubmitReviewDto
{
    [Required]
    [RegularExpression("^(Again|Hard|Good|Easy)$", ErrorMessage = "Rating must be Again, Hard, Good, or Easy.")]
    public string Rating { get; init; } = string.Empty;

    [Range(0, 3600)]
    public int DurationSeconds { get; init; }
}
