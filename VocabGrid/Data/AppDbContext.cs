using Microsoft.EntityFrameworkCore;
using VocabGrid.Entities;

namespace VocabGrid.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<QuizSession> QuizSessions { get; set; }
        public DbSet<QuizSessionAnswer> QuizSessionAnswers { get; set; }
        public DbSet<LessonVocabulary> LessonVocabularies { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<UserWordProgress> UserWordProgresses { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<LearningPurpose> LearningPurposes { get; set; }
        public DbSet<UserLearningPurpose> UserLearningPurposes { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<StudyActivity> StudyActivities { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.GoogleId)
                .IsUnique()
                .HasFilter("[GoogleId] IS NOT NULL");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.AppleId)
                .IsUnique()
                .HasFilter("[AppleId] IS NOT NULL");

            modelBuilder.Entity<UserSettings>()
                .HasOne(s => s.User)
                .WithOne(u => u.Settings)
                .HasForeignKey<UserSettings>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSettings>()
                .HasIndex(s => s.UserId)
                .IsUnique();

            modelBuilder.Entity<LessonVocabulary>()
                .HasKey(lv => new { lv.LessonID, lv.WordID });

            modelBuilder.Entity<LessonVocabulary>()
                .HasOne(lv => lv.Lesson)
                .WithMany(l => l.LessonVocabularies)
                .HasForeignKey(lv => lv.LessonID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LessonVocabulary>()
                .HasOne(lv => lv.Vocabulary)
                .WithMany(v => v.LessonVocabularies)
                .HasForeignKey(lv => lv.WordID)
                .OnDelete(DeleteBehavior.Cascade);

            // NoAction on User: SQL Server rejects Users->UWP and Users->Decks->Vocabularies->UWP both Cascade.
            // Progress rows still cascade-delete when the Vocabulary (card) is removed with the Deck.
            modelBuilder.Entity<UserWordProgress>()
                .HasOne(uwp => uwp.User)
                .WithMany(u => u.UserWordProgresses)
                .HasForeignKey(uwp => uwp.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserWordProgress>()
                .HasOne(uwp => uwp.Vocabulary)
                .WithMany(v => v.UserWordProgresses)
                .HasForeignKey(uwp => uwp.WordID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserWordProgress>()
                .HasIndex(uwp => new { uwp.UserID, uwp.WordID })
                .IsUnique();

            modelBuilder.Entity<UserProgress>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProgresses)
                .HasForeignKey(up => up.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserProgress>()
                .HasOne(up => up.Lesson)
                .WithMany(l => l.UserProgresses)
                .HasForeignKey(up => up.LessonID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserProgress>()
                .HasIndex(up => new { up.UserID, up.LessonID })
                .IsUnique();

            modelBuilder.Entity<UserCategory>()
                .HasKey(uc => new { uc.UserId, uc.CategoryId });

            modelBuilder.Entity<UserCategory>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCategories)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCategory>()
                .HasOne(uc => uc.Category)
                .WithMany(c => c.UserCategories)
                .HasForeignKey(uc => uc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLearningPurpose>()
                .HasKey(ulp => new { ulp.UserId, ulp.LearningPurposeId });

            modelBuilder.Entity<UserLearningPurpose>()
                .HasOne(ulp => ulp.User)
                .WithMany(u => u.UserLearningPurposes)
                .HasForeignKey(ulp => ulp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLearningPurpose>()
                .HasOne(ulp => ulp.LearningPurpose)
                .WithMany(lp => lp.UserLearningPurposes)
                .HasForeignKey(ulp => ulp.LearningPurposeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserBadge>()
                .HasKey(ub => new { ub.UserId, ub.BadgeId });

            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBadges)
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.Badge)
                .WithMany(b => b.UserBadges)
                .HasForeignKey(ub => ub.BadgeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Deck>()
                .HasOne(d => d.User)
                .WithMany(u => u.Decks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Deck>()
                .Property(d => d.Title)
                .HasMaxLength(50);

            // Figma: deck silinince kartlar da silinir. SQL Server multiple-cascade-path
            // kısıtı nedeniyle DB'de NoAction; EF ClientCascade ile kartları önce siler.
            modelBuilder.Entity<Vocabulary>()
                .HasOne(v => v.Deck)
                .WithMany(d => d.Flashcards)
                .HasForeignKey(v => v.DeckId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Lesson)
                .WithMany(l => l.Quizzes)
                .HasForeignKey(q => q.LessonID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuizOption>()
                .HasOne(o => o.Quiz)
                .WithMany(q => q.Options)
                .HasForeignKey(o => o.QuizID)
                .OnDelete(DeleteBehavior.Cascade);

            // NoAction: same SQL Server multiple-cascade-path limit as StudyActivity (via Decks).
            modelBuilder.Entity<QuizSession>()
                .HasOne(s => s.User)
                .WithMany(u => u.QuizSessions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<QuizSession>()
                .HasOne(s => s.Lesson)
                .WithMany()
                .HasForeignKey(s => s.LessonId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<QuizSession>()
                .HasOne(s => s.Deck)
                .WithMany()
                .HasForeignKey(s => s.DeckId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<QuizSessionAnswer>()
                .HasOne(a => a.QuizSession)
                .WithMany(s => s.Answers)
                .HasForeignKey(a => a.QuizSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuizSessionAnswer>()
                .HasOne(a => a.Quiz)
                .WithMany()
                .HasForeignKey(a => a.QuizId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<QuizSessionAnswer>()
                .HasOne(a => a.SelectedOption)
                .WithMany()
                .HasForeignKey(a => a.SelectedOptionId)
                .OnDelete(DeleteBehavior.NoAction);

            // NoAction: SQL Server rejects multiple cascade paths (Users -> StudyActivities and Users -> Decks -> StudyActivities).
            modelBuilder.Entity<StudyActivity>()
                .HasOne(a => a.User)
                .WithMany(u => u.StudyActivities)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudyActivity>()
                .HasOne(a => a.Vocabulary)
                .WithMany()
                .HasForeignKey(a => a.WordId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudyActivity>()
                .HasOne(a => a.Lesson)
                .WithMany()
                .HasForeignKey(a => a.LessonId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudyActivity>()
                .HasOne(a => a.Deck)
                .WithMany()
                .HasForeignKey(a => a.DeckId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudyActivity>()
                .HasIndex(a => new { a.UserId, a.OccurredAt });

            modelBuilder.Entity<PasswordResetToken>()
                .HasOne(t => t.User)
                .WithMany(u => u.PasswordResetTokens)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PasswordResetToken>()
                .HasIndex(t => t.Token)
                .IsUnique();

            // Figma Profile categories (Science dahil)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food", Description = "Food & Dining Vocabulary" },
                new Category { Id = 2, Name = "Travel", Description = "Travel & Tourism Vocabulary" },
                new Category { Id = 3, Name = "Business", Description = "Professional & Work Vocabulary" },
                new Category { Id = 4, Name = "Technology", Description = "Tech & IT Vocabulary" },
                new Category { Id = 5, Name = "Education", Description = "Academic & School Vocabulary" },
                new Category { Id = 6, Name = "Movies", Description = "Cinema & Entertainment" },
                new Category { Id = 7, Name = "Music", Description = "Music & Songs" },
                new Category { Id = 8, Name = "Gaming", Description = "Video Games & Gaming Culture" },
                new Category { Id = 9, Name = "Sports", Description = "Sports & Fitness" },
                new Category { Id = 10, Name = "Health", Description = "Health & Medicine" },
                new Category { Id = 11, Name = "Shopping", Description = "Shopping & Fashion" },
                new Category { Id = 12, Name = "Family", Description = "Family & Relationships" },
                new Category { Id = 13, Name = "Nature", Description = "Nature & Environment" },
                new Category { Id = 14, Name = "Science", Description = "Science & Research" },
                new Category { Id = 15, Name = "Animals", Description = "Animals & Wildlife" }
            );

            // Figma: Learning Purpose · 2 selected (çoklu seçim)
            modelBuilder.Entity<LearningPurpose>().HasData(
                new LearningPurpose { Id = 1, Name = "Travel", Description = "Travel and tourism" },
                new LearningPurpose { Id = 2, Name = "Business", Description = "Work and professional use" },
                new LearningPurpose { Id = 3, Name = "Academic", Description = "School and exams" },
                new LearningPurpose { Id = 4, Name = "Daily Conversation", Description = "Everyday speaking" },
                new LearningPurpose { Id = 5, Name = "Culture", Description = "Media, culture and hobbies" },
                new LearningPurpose { Id = 6, Name = "Relocation", Description = "Living abroad" }
            );

            modelBuilder.Entity<Badge>().HasData(
                new Badge
                {
                    Id = 1,
                    Name = "7-Day Streak",
                    Description = "Study 7 days in a row",
                    Icon = "flame_icon",
                    UnlockCondition = "StreakDays",
                    Threshold = 7
                },
                new Badge
                {
                    Id = 2,
                    Name = "Perfect Score",
                    Description = "Get 100% on a quiz",
                    Icon = "star_icon",
                    UnlockCondition = "PerfectQuiz",
                    Threshold = 100
                },
                new Badge
                {
                    Id = 3,
                    Name = "Word Collector",
                    Description = "Learn 100 words",
                    Icon = "book_icon",
                    UnlockCondition = "WordsLearned",
                    Threshold = 100
                },
                new Badge
                {
                    Id = 4,
                    Name = "Speed Learner",
                    Description = "Finish 20 cards in 5 min",
                    Icon = "zap_icon",
                    UnlockCondition = "CardsInMinutes",
                    Threshold = 20
                },
                new Badge
                {
                    Id = 5,
                    Name = "Polyglot",
                    Description = "Start a 2nd language",
                    Icon = "globe_icon",
                    UnlockCondition = "LanguagesStarted",
                    Threshold = 2
                }
            );
        }
    }
}
