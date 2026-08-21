using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VocabGrid.Migrations
{
    /// <inheritdoc />
    public partial class AddFsrsFieldsToUserWordProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Difficulty",
                table: "UserWordProgresses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Stability",
                table: "UserWordProgresses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            // One-time backfill so a card someone has already reviewed under
            // the old SM-2-style scheduler isn't treated as brand-new the
            // next time it's reviewed (FsrsEngine treats Stability<=0 as
            // "never reviewed"). This is an approximation -- there's no exact
            // mathematical equivalence between the two models -- but it's a
            // reasonable one-time seed: IntervalDays is a decent proxy for
            // how long a memory has already been surviving between reviews,
            // and EaseFactor (1.3=hardest..3.0=easiest) inverts cleanly onto
            // FSRS's Difficulty scale (1=easiest..10=hardest). Rows with
            // ReviewCount = 0 are genuinely never-reviewed and are left at
            // the column default (0), correctly routing them through
            // FsrsEngine's first-review path instead.
            migrationBuilder.Sql(@"
                UPDATE UserWordProgresses
                SET Stability = CASE WHEN IntervalDays > 0 THEN CAST(IntervalDays AS FLOAT) ELSE 1.0 END,
                    Difficulty = 10.0 - ((EaseFactor - 1.3) / 1.7) * 9.0
                WHERE ReviewCount > 0;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "UserWordProgresses");

            migrationBuilder.DropColumn(
                name: "Stability",
                table: "UserWordProgresses");
        }
    }
}
