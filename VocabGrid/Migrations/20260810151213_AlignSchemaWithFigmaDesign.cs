using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VocabGrid.Migrations
{
    /// <inheritdoc />
    public partial class AlignSchemaWithFigmaDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vocabularies_Decks_DeckId",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "LearningPurpose",
                table: "UserSettings");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Vocabularies",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Vocabularies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LongestStreak",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TargetProficiencyLevel",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Beginner");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TimeLimitSeconds",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Decks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Decks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LearningPurposes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPurposes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: true),
                    DeckId = table.Column<int>(type: "int", nullable: true),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    CorrectCount = table.Column<int>(type: "int", nullable: false),
                    WrongCount = table.Column<int>(type: "int", nullable: false),
                    SkippedCount = table.Column<int>(type: "int", nullable: false),
                    ScorePoints = table.Column<int>(type: "int", nullable: false),
                    TimeLimitSeconds = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizSessions_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_QuizSessions_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_QuizSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserLearningPurposes",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LearningPurposeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLearningPurposes", x => new { x.UserId, x.LearningPurposeId });
                    table.ForeignKey(
                        name: "FK_UserLearningPurposes_LearningPurposes_LearningPurposeId",
                        column: x => x.LearningPurposeId,
                        principalTable: "LearningPurposes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLearningPurposes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizSessionAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizSessionId = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: true),
                    SelectedOptionId = table.Column<int>(type: "int", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true),
                    IsSkipped = table.Column<bool>(type: "bit", nullable: false),
                    TimeSpentSeconds = table.Column<int>(type: "int", nullable: false),
                    PointsEarned = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizSessionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizSessionAnswers_QuizOptions_SelectedOptionId",
                        column: x => x.SelectedOptionId,
                        principalTable: "QuizOptions",
                        principalColumn: "OptionID");
                    table.ForeignKey(
                        name: "FK_QuizSessionAnswers_QuizSessions_QuizSessionId",
                        column: x => x.QuizSessionId,
                        principalTable: "QuizSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizSessionAnswers_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Science & Research", "Science" });

            migrationBuilder.InsertData(
                table: "LearningPurposes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Travel and tourism", "Travel" },
                    { 2, "Work and professional use", "Business" },
                    { 3, "School and exams", "Academic" },
                    { 4, "Everyday speaking", "Daily Conversation" },
                    { 5, "Media, culture and hobbies", "Culture" },
                    { 6, "Living abroad", "Relocation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizSessionAnswers_QuizId",
                table: "QuizSessionAnswers",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSessionAnswers_QuizSessionId",
                table: "QuizSessionAnswers",
                column: "QuizSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSessionAnswers_SelectedOptionId",
                table: "QuizSessionAnswers",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSessions_DeckId",
                table: "QuizSessions",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSessions_LessonId",
                table: "QuizSessions",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSessions_UserId",
                table: "QuizSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLearningPurposes_LearningPurposeId",
                table: "UserLearningPurposes",
                column: "LearningPurposeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vocabularies_Decks_DeckId",
                table: "Vocabularies",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vocabularies_Decks_DeckId",
                table: "Vocabularies");

            migrationBuilder.DropTable(
                name: "QuizSessionAnswers");

            migrationBuilder.DropTable(
                name: "UserLearningPurposes");

            migrationBuilder.DropTable(
                name: "QuizSessions");

            migrationBuilder.DropTable(
                name: "LearningPurposes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LongestStreak",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TargetProficiencyLevel",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "TimeLimitSeconds",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Decks");

            migrationBuilder.AddColumn<string>(
                name: "LearningPurpose",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Decks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name" },
                values: new object[] { "School & Classroom Vocabulary", "School" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vocabularies_Decks_DeckId",
                table: "Vocabularies",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
