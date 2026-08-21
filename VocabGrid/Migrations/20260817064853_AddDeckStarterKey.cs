using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VocabGrid.Migrations
{
    /// <inheritdoc />
    public partial class AddDeckStarterKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StarterKey",
                table: "Decks",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarterKey",
                table: "Decks");
        }
    }
}
