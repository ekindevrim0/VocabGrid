using VocabGrid.Entities;
using VocabGrid.Interfaces;

namespace VocabGrid.Services;

/// <summary>
/// Ham çalışma aktivitesini günlük özete işler.
///
/// Her aktivite iki yere yazılır: ayrıntının durduğu <see cref="StudyActivity"/>
/// ve gün başına tek satır tutan <see cref="DailyStudySummary"/>. İkincisi
/// türetilmiş veridir — istatistik ekranının bir yıllık ham aktiviteyi taramak
/// zorunda kalmaması için var.
///
/// Bir aktivite kaydedilirken bu çağrılmazsa özet o gün için eksik kalır; ham
/// veri yerinde durduğu için sonuçlar yeniden hesaplanabilir, ama ekran o güne
/// kadar yanlış gösterir. Bu yüzden çağrı, aktivitenin eklendiği yerin hemen
/// yanında durur.
/// </summary>
internal static class DailySummaryEngine
{
    internal static async Task RecordAsync(IUnitOfWork unitOfWork, StudyActivity activity)
    {
        var day = DateOnly.FromDateTime(activity.OccurredAt);
        var repository = unitOfWork.Repository<DailyStudySummary>();

        var summary = (await repository.FindAsync(s => s.UserId == activity.UserId && s.Day == day))
            .FirstOrDefault();

        var isNewRow = summary is null;
        if (summary is null)
        {
            summary = new DailyStudySummary { UserId = activity.UserId, Day = day };
            await repository.AddAsync(summary);
        }

        switch (activity.ActivityType)
        {
            case "Review":
                summary.ReviewCount++;
                // "Again" tekrar görülmesi gereken kart demek; isabet sayısına
                // girmemeli. Diğer üç değerlendirme (Hard/Medium/Easy) hepsi
                // hatırlandı anlamına gelir.
                if (activity.Result is not null && activity.Result != "Again")
                {
                    summary.CorrectCount++;
                }
                break;

            case "Quiz":
                summary.QuizCount++;
                break;

            case "Lesson":
                summary.LessonCount++;
                break;
        }

        summary.StudySeconds += activity.DurationSeconds;
        summary.XpEarned += activity.XpEarned;
        summary.UpdatedAt = DateTime.UtcNow;

        // Yalnızca var olan satırda. Yeni eklenen satır hâlâ Added durumunda ve
        // anahtarı geçici; EF üzerinde Update çağrılırsa "temporary value while
        // attempting to change the entity's state to 'Modified'" hatası verir.
        // Zaten gerek de yok — Added varlığın alanlarındaki değişiklikler
        // SaveChanges'te INSERT'e girer.
        if (!isNewRow)
        {
            repository.Update(summary);
        }
    }
}
