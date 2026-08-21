using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VocabGrid.Migrations
{
    /// <inheritdoc />
    public partial class AddDeckLanguageCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "Decks",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            // Backfill for decks that already existed before this column did.
            // 1) A StarterKey ("starter_basics_DE", "category_food_de") always
            //    ends in the deck's actual language code -- trust that first.
            migrationBuilder.Sql(@"
                UPDATE Decks
                SET LanguageCode = LOWER(RIGHT(StarterKey, CHARINDEX('_', REVERSE(StarterKey)) - 1))
                WHERE StarterKey IS NOT NULL AND CHARINDEX('_', REVERSE(StarterKey)) > 0;
            ");

            // 2) Anything still unset (the learner's own decks, which never
            //    had a StarterKey) falls back to the owning account's current
            //    target language -- the same flag-code-to-ISO resolution
            //    CategoryDeckSynchronizer.ResolveLanguageCode applies, since a
            //    raw code column can't call C# and most custom decks were
            //    made in whatever the learner is (or was, until they retired
            //    it further below) still studying.
            migrationBuilder.Sql(@"
                UPDATE d
                SET d.LanguageCode = CASE LOWER(u.TargetLanguageCode)
                    WHEN 'gb' THEN 'en'
                    WHEN 'jp' THEN 'ja'
                    WHEN 'kr' THEN 'ko'
                    WHEN 'cn' THEN 'zh'
                    ELSE LOWER(u.TargetLanguageCode)
                END
                FROM Decks d
                JOIN Users u ON u.Id = d.UserId
                WHERE d.LanguageCode IS NULL;
            ");

            // 3) One known exception the two rules above get wrong: this
            // specific deck's actual content is Turkish, but it predates
            // StarterKey tagging and its owner's target has since moved to
            // German, so rule 2 would mislabel it. Confirmed by hand while
            // fixing the underlying bug (2026-08-21).
            migrationBuilder.Sql("UPDATE Decks SET LanguageCode = 'tr' WHERE Id = 1033;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "Decks");
        }
    }
}
