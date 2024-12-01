using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class udNt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Natiton",
                table: "ArtistPendings");

            migrationBuilder.AddColumn<string>(
                name: "Nation",
                table: "ArtistPendings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nation",
                table: "ArtistPendings");

            migrationBuilder.AddColumn<string>(
                name: "Natiton",
                table: "ArtistPendings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
