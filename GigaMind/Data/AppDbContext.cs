using Microsoft.EntityFrameworkCore;
using GigaMind.API.Entities;

namespace GigaMind.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Vocabulary> Vocabularies { get; set; }
    public DbSet<LessonVocabulary> LessonVocabularies { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizOption> QuizOptions { get; set; }
    public DbSet<UserProgress> UserProgresses { get; set; }
    public DbSet<UserWordProgress> UserWordProgresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // LessonVocabulary (Çoktan çoğa ilişki için composite key)
        modelBuilder.Entity<LessonVocabulary>()
            .HasKey(lv => new { lv.LessonID, lv.WordID });

        modelBuilder.Entity<LessonVocabulary>()
            .HasOne(lv => lv.Lesson)
            .WithMany(l => l.LessonVocabularies)
            .HasForeignKey(lv => lv.LessonID);

        modelBuilder.Entity<LessonVocabulary>()
            .HasOne(lv => lv.Vocabulary)
            .WithMany(v => v.LessonVocabularies)
            .HasForeignKey(lv => lv.WordID);
    }
}