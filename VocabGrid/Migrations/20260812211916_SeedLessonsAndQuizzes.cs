using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VocabGrid.Migrations
{
    /// <inheritdoc />
    public partial class SeedLessonsAndQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "LessonID", "CreatedAt", "Description", "Level", "OrderIndex", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Basic hello and introductions", "A1", 1, "Greetings" },
                    { 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Common food and drink words", "A1", 2, "Food Basics" }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "QuizID", "ImageUrl", "LessonID", "Points", "QuestionText", "QuestionType", "TimeLimitSeconds" },
                values: new object[,]
                {
                    { 1, null, 1, 1, "What does 'Merhaba' mean?", "MultipleChoice", 20 },
                    { 2, null, 1, 1, "How do you say 'Good morning' in Turkish?", "MultipleChoice", 20 },
                    { 3, null, 1, 1, "What does 'Teşekkürler' mean?", "MultipleChoice", 20 },
                    { 4, null, 1, 1, "How do you say 'My name is...' in Turkish?", "MultipleChoice", 20 },
                    { 5, null, 1, 1, "What does 'Güle güle' mean?", "MultipleChoice", 20 },
                    { 6, null, 2, 1, "What does 'Elma' mean?", "MultipleChoice", 20 },
                    { 7, null, 2, 1, "How do you say 'Water' in Turkish?", "MultipleChoice", 20 },
                    { 8, null, 2, 1, "What does 'Ekmek' mean?", "MultipleChoice", 20 },
                    { 9, null, 2, 1, "How do you say 'Tea' in Turkish?", "MultipleChoice", 20 },
                    { 10, null, 2, 1, "What does 'Süt' mean?", "MultipleChoice", 20 }
                });

            migrationBuilder.InsertData(
                table: "QuizOptions",
                columns: new[] { "OptionID", "IsCorrect", "OptionText", "QuizID" },
                values: new object[,]
                {
                    { 1, true, "Hello", 1 },
                    { 2, false, "Goodbye", 1 },
                    { 3, false, "Please", 1 },
                    { 4, false, "Sorry", 1 },
                    { 5, false, "İyi geceler", 2 },
                    { 6, true, "Günaydın", 2 },
                    { 7, false, "İyi akşamlar", 2 },
                    { 8, false, "Hoşça kal", 2 },
                    { 9, false, "You're welcome", 3 },
                    { 10, true, "Thank you", 3 },
                    { 11, false, "Excuse me", 3 },
                    { 12, false, "Congratulations", 3 },
                    { 13, true, "Benim adım...", 4 },
                    { 14, false, "Nasılsın?", 4 },
                    { 15, false, "Nerelisin?", 4 },
                    { 16, false, "Kaç yaşındasın?", 4 },
                    { 17, false, "Welcome", 5 },
                    { 18, true, "Goodbye (to someone leaving)", 5 },
                    { 19, false, "See you tomorrow", 5 },
                    { 20, false, "Good night", 5 },
                    { 21, false, "Banana", 6 },
                    { 22, true, "Apple", 6 },
                    { 23, false, "Orange", 6 },
                    { 24, false, "Grape", 6 },
                    { 25, true, "Su", 7 },
                    { 26, false, "Çay", 7 },
                    { 27, false, "Kahve", 7 },
                    { 28, false, "Süt", 7 },
                    { 29, false, "Cheese", 8 },
                    { 30, true, "Bread", 8 },
                    { 31, false, "Rice", 8 },
                    { 32, false, "Meat", 8 },
                    { 33, false, "Kahve", 9 },
                    { 34, false, "Su", 9 },
                    { 35, true, "Çay", 9 },
                    { 36, false, "Meyve suyu", 9 },
                    { 37, true, "Milk", 10 },
                    { 38, false, "Sugar", 10 },
                    { 39, false, "Salt", 10 },
                    { 40, false, "Honey", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "QuizOptions",
                keyColumn: "OptionID",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 2);
        }
    }
}
