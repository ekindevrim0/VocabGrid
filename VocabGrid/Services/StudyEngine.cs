using VocabGrid.Entities;
using VocabGrid.Interfaces;

namespace VocabGrid.Services;

public sealed record ReviewSchedule(int IntervalDays, double EaseFactor, DateTime NextReviewDate, int MasteryDelta);

public static class StudyEngine
{
    public static ReviewSchedule CalculateReviewSchedule(
        int currentIntervalDays,
        double currentEaseFactor,
        string rating,
        DateTime reviewedAt)
    {
        var interval = Math.Max(0, currentIntervalDays);
        var ease = Math.Clamp(currentEaseFactor <= 0 ? 2.5 : currentEaseFactor, 1.3, 3.0);

        return rating switch
        {
            "Again" => new ReviewSchedule(0, Math.Max(1.3, ease - 0.20), reviewedAt.AddMinutes(10), -1),
            "Hard" => new ReviewSchedule(
                Math.Max(1, interval == 0 ? 1 : (int)Math.Ceiling(interval * 1.2)),
                Math.Max(1.3, ease - 0.15),
                reviewedAt.AddDays(Math.Max(1, interval == 0 ? 1 : (int)Math.Ceiling(interval * 1.2))),
                0),
            "Good" => new ReviewSchedule(
                interval == 0 ? 1 : Math.Max(1, (int)Math.Round(interval * ease)),
                ease,
                reviewedAt.AddDays(interval == 0 ? 1 : Math.Max(1, (int)Math.Round(interval * ease))),
                1),
            "Easy" => new ReviewSchedule(
                interval == 0 ? 4 : Math.Max(1, (int)Math.Round(interval * (ease + 0.15))),
                Math.Min(3.0, ease + 0.15),
                reviewedAt.AddDays(interval == 0 ? 4 : Math.Max(1, (int)Math.Round(interval * (ease + 0.15)))),
                2),
            _ => throw new ArgumentOutOfRangeException(nameof(rating), "Unsupported review rating.")
        };
    }

    public static async Task UpdateStreakAsync(IUnitOfWork unitOfWork, User user, DateTime activityDate)
    {
        var activities = await unitOfWork.Repository<StudyActivity>()
            .FindAsync(activity => activity.UserId == user.Id);

        var dates = activities
            .Select(activity => activity.OccurredAt.Date)
            .Append(activityDate.Date)
            .Distinct()
            .OrderBy(date => date)
            .ToList();

        user.CurrentStreak = CalculateCurrentStreak(dates, activityDate);
        user.LongestStreak = Math.Max(user.LongestStreak, CalculateLongestStreak(dates));
        unitOfWork.Repository<User>().Update(user);
    }

    public static int CalculateCurrentStreak(IEnumerable<DateTime> activityDates, DateTime referenceDate)
    {
        var dates = activityDates.Select(date => date.Date).ToHashSet();
        var cursor = referenceDate.Date;

        if (!dates.Contains(cursor))
        {
            cursor = cursor.AddDays(-1);
        }

        var streak = 0;
        while (dates.Contains(cursor))
        {
            streak++;
            cursor = cursor.AddDays(-1);
        }

        return streak;
    }

    public static int CalculateLongestStreak(IEnumerable<DateTime> activityDates)
    {
        var dates = activityDates.Select(date => date.Date).Distinct().OrderBy(date => date).ToList();
        if (dates.Count == 0)
        {
            return 0;
        }

        var longest = 1;
        var current = 1;

        for (var index = 1; index < dates.Count; index++)
        {
            if (dates[index] == dates[index - 1].AddDays(1))
            {
                current++;
                longest = Math.Max(longest, current);
            }
            else
            {
                current = 1;
            }
        }

        return longest;
    }
}
