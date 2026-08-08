using Microsoft.EntityFrameworkCore;
using VocabGrid.API.Entities;
using VocabGrid.Entities;

namespace VocabGrid.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<LessonVocabulary> LessonVocabularies { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<UserWordProgress> UserWordProgresses { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Key for LessonVocabulary
            modelBuilder.Entity<LessonVocabulary>()
                .HasKey(lv => new { lv.LessonID, lv.WordID });

            // Composite Key & Relationships for UserCategory
            modelBuilder.Entity<UserCategory>()
                .HasKey(uc => new { uc.UserId, uc.CategoryId });

            modelBuilder.Entity<UserCategory>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCategories)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCategory>()
                .HasOne(uc => uc.Category)
                .WithMany(c => c.UserCategories)
                .HasForeignKey(uc => uc.CategoryId);

            // Seed Categories Data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food", Description = "Food & Dining Vocabulary", IsSelected = true },
                new Category { Id = 2, Name = "Travel", Description = "Travel & Tourism Vocabulary", IsSelected = true },
                new Category { Id = 3, Name = "Business", Description = "Professional & Work Vocabulary", IsSelected = true },
                new Category { Id = 4, Name = "Technology", Description = "Tech & IT Vocabulary", IsSelected = false },
                new Category { Id = 5, Name = "Education", Description = "Academic & School Vocabulary", IsSelected = false },
                new Category { Id = 6, Name = "Movies", Description = "Cinema & Entertainment", IsSelected = false },
                new Category { Id = 7, Name = "Music", Description = "Music & Songs", IsSelected = false },
                new Category { Id = 8, Name = "Gaming", Description = "Video Games & Gaming Culture", IsSelected = false },
                new Category { Id = 9, Name = "Sports", Description = "Sports & Fitness", IsSelected = false },
                new Category { Id = 10, Name = "Health", Description = "Health & Medicine", IsSelected = false },
                new Category { Id = 11, Name = "Shopping", Description = "Shopping & Fashion", IsSelected = false },
                new Category { Id = 12, Name = "Family", Description = "Family & Relationships", IsSelected = false },
                new Category { Id = 13, Name = "Nature", Description = "Nature & Environment", IsSelected = false },
                new Category { Id = 14, Name = "Science", Description = "Science & Research", IsSelected = false },
                new Category { Id = 15, Name = "Animals", Description = "Animals & Wildlife", IsSelected = false }
            );

            // Seed Badges Data
            modelBuilder.Entity<Badge>().HasData(
                new Badge { Id = 1, Name = "7-Day Streak", Description = "Study 7 days in a row", Icon = "flame_icon", IsEarned = true },
                new Badge { Id = 2, Name = "Perfect Score", Description = "Get 100% on a quiz", Icon = "star_icon", IsEarned = true },
                new Badge { Id = 3, Name = "Word Collector", Description = "Learn 100 words", Icon = "book_icon", IsEarned = true },
                new Badge { Id = 4, Name = "Speed Learner", Description = "Finish 20 cards in 5 min", Icon = "zap_icon", IsEarned = false },
                new Badge { Id = 5, Name = "Polyglot", Description = "Start a 2nd language", Icon = "globe_icon", IsEarned = false }
            );
        }
    }
}