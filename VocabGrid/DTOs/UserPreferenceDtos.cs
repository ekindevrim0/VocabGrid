using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public class ReplaceUserCategoriesDto
{
    [Required]
    public List<int> CategoryIds { get; set; } = new();
}

public class ReplaceUserLearningPurposesDto
{
    [Required]
    public List<int> LearningPurposeIds { get; set; } = new();
}
