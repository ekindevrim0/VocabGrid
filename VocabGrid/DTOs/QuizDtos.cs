using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public sealed class StartQuizSessionDto
{
    [Range(1, int.MaxValue)]
    public int LessonId { get; init; }

    [Range(1, 50)]
    public int QuestionCount { get; init; } = 5;
}
public sealed class SubmitQuizAnswerDto
{
    [Range(1, int.MaxValue)]
    public int QuizId { get; init; }

    [Range(1, int.MaxValue)]
    public int? SelectedOptionId { get; init; }

    public bool Skip { get; init; }

    [Range(0, 3600)]
    public int TimeSpentSeconds { get; init; }
}
