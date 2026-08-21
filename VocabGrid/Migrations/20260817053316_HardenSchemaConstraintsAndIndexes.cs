using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VocabGrid.Migrations
{
    /// <inheritdoc />
    public partial class HardenSchemaConstraintsAndIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vocabularies_DeckId",
                table: "Vocabularies");

            migrationBuilder.DropIndex(
                name: "IX_Decks_UserId",
                table: "Decks");

            migrationBuilder.AlterColumn<string>(
                name: "Translation",
                table: "Vocabularies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Term",
                table: "Vocabularies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Vocabularies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExampleSentence",
                table: "Vocabularies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AudioUrl",
                table: "Vocabularies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastRating",
                table: "UserWordProgresses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThemeColor",
                table: "UserSettings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TextSize",
                table: "UserSettings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DifficultyMode",
                table: "UserSettings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TargetProficiencyLevel",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TargetLanguage",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NativeLanguage",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "StudyActivities",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActivityType",
                table: "StudyActivities",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionType",
                table: "Quizzes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionText",
                table: "Quizzes",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Quizzes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OptionText",
                table: "QuizOptions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Lessons",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lessons",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LearningPurposes",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LearningPurposes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Decks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CoverImageUrl",
                table: "Decks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UnlockCondition",
                table: "Badges",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Badges",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Badges",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Badges",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Vocabularies_DeckId_Term",
                table: "Vocabularies",
                columns: new[] { "DeckId", "Term" });

            migrationBuilder.CreateIndex(
                name: "IX_Vocabularies_Term",
                table: "Vocabularies",
                column: "Term");

            migrationBuilder.CreateIndex(
                name: "IX_Vocabularies_Translation",
                table: "Vocabularies",
                column: "Translation");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Vocabulary_NotBlank",
                table: "Vocabularies",
                sql: "LEN(LTRIM(RTRIM([Term]))) > 0 AND LEN(LTRIM(RTRIM([Translation]))) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_UserWordProgress_Counters",
                table: "UserWordProgresses",
                sql: "[ReviewCount] >= 0 AND [IntervalDays] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_UserWordProgress_EaseFactor",
                table: "UserWordProgresses",
                sql: "[EaseFactor] BETWEEN 1.3 AND 3.0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_UserWordProgress_LastRating",
                table: "UserWordProgresses",
                sql: "[LastRating] IS NULL OR [LastRating] IN ('Again', 'Hard', 'Medium', 'Easy')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_UserWordProgress_MasteryLevel",
                table: "UserWordProgresses",
                sql: "[MasteryLevel] BETWEEN 0 AND 5");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Users_Counters",
                table: "Users",
                sql: "[TotalXp] >= 0 AND [CurrentStreak] >= 0 AND [LongestStreak] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Users_DailyGoalMinutes",
                table: "Users",
                sql: "[DailyGoalMinutes] BETWEEN 1 AND 600");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Users_Level",
                table: "Users",
                sql: "[Level] >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Users_TargetProficiencyLevel",
                table: "Users",
                sql: "[TargetProficiencyLevel] IN ('Just Starting', 'Beginner', 'Intermediate', 'Advanced')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_UserProgress_Score",
                table: "UserProgresses",
                sql: "[Score] BETWEEN 0 AND 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_StudyActivity_ActivityType",
                table: "StudyActivities",
                sql: "[ActivityType] IN ('Review', 'Lesson', 'Quiz')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_StudyActivity_Counters",
                table: "StudyActivities",
                sql: "[DurationSeconds] >= 0 AND [XpEarned] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Quiz_Points",
                table: "Quizzes",
                sql: "[Points] >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Quiz_QuestionType",
                table: "Quizzes",
                sql: "[QuestionType] IN ('MultipleChoice', 'TrueFalse', 'FillInTheBlank')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Quiz_TimeLimitSeconds",
                table: "Quizzes",
                sql: "[TimeLimitSeconds] BETWEEN 5 AND 600");

            migrationBuilder.AddCheckConstraint(
                name: "CK_QuizSession_Counters",
                table: "QuizSessions",
                sql: "[TotalQuestions] >= 0 AND [CorrectCount] >= 0 AND [WrongCount] >= 0 AND [SkippedCount] >= 0 AND [ScorePoints] >= 0");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_OrderIndex",
                table: "Lessons",
                column: "OrderIndex");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Lesson_Level",
                table: "Lessons",
                sql: "[Level] IN ('A1', 'A2', 'B1', 'B2', 'C1', 'C2')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Lesson_OrderIndex",
                table: "Lessons",
                sql: "[OrderIndex] >= 0");

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationTokens_ExpiresAt",
                table: "EmailVerificationTokens",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_UserId_CreatedAt",
                table: "Decks",
                columns: new[] { "UserId", "CreatedAt" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Deck_TitleNotBlank",
                table: "Decks",
                sql: "LEN(LTRIM(RTRIM([Title]))) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Badge_Threshold",
                table: "Badges",
                sql: "[Threshold] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vocabularies_DeckId_Term",
                table: "Vocabularies");

            migrationBuilder.DropIndex(
                name: "IX_Vocabularies_Term",
                table: "Vocabularies");

            migrationBuilder.DropIndex(
                name: "IX_Vocabularies_Translation",
                table: "Vocabularies");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Vocabulary_NotBlank",
                table: "Vocabularies");

            migrationBuilder.DropCheckConstraint(
                name: "CK_UserWordProgress_Counters",
                table: "UserWordProgresses");

            migrationBuilder.DropCheckConstraint(
                name: "CK_UserWordProgress_EaseFactor",
                table: "UserWordProgresses");

            migrationBuilder.DropCheckConstraint(
                name: "CK_UserWordProgress_LastRating",
                table: "UserWordProgresses");

            migrationBuilder.DropCheckConstraint(
                name: "CK_UserWordProgress_MasteryLevel",
                table: "UserWordProgresses");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Users_Counters",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Users_DailyGoalMinutes",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Users_Level",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Users_TargetProficiencyLevel",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_UserProgress_Score",
                table: "UserProgresses");

            migrationBuilder.DropCheckConstraint(
                name: "CK_StudyActivity_ActivityType",
                table: "StudyActivities");

            migrationBuilder.DropCheckConstraint(
                name: "CK_StudyActivity_Counters",
                table: "StudyActivities");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Quiz_Points",
                table: "Quizzes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Quiz_QuestionType",
                table: "Quizzes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Quiz_TimeLimitSeconds",
                table: "Quizzes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_QuizSession_Counters",
                table: "QuizSessions");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_OrderIndex",
                table: "Lessons");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Lesson_Level",
                table: "Lessons");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Lesson_OrderIndex",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_EmailVerificationTokens_ExpiresAt",
                table: "EmailVerificationTokens");

            migrationBuilder.DropIndex(
                name: "IX_Decks_UserId_CreatedAt",
                table: "Decks");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Deck_TitleNotBlank",
                table: "Decks");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Badge_Threshold",
                table: "Badges");

            migrationBuilder.AlterColumn<string>(
                name: "Translation",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Term",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExampleSentence",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AudioUrl",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastRating",
                table: "UserWordProgresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThemeColor",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "TextSize",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "DifficultyMode",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "TargetProficiencyLevel",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "TargetLanguage",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NativeLanguage",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "StudyActivities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActivityType",
                table: "StudyActivities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "QuestionType",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "QuestionText",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OptionText",
                table: "QuizOptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LearningPurposes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LearningPurposes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Decks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "CoverImageUrl",
                table: "Decks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "UnlockCondition",
                table: "Badges",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Badges",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Badges",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Badges",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.CreateIndex(
                name: "IX_Vocabularies_DeckId",
                table: "Vocabularies",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_UserId",
                table: "Decks",
                column: "UserId");
        }
    }
}
