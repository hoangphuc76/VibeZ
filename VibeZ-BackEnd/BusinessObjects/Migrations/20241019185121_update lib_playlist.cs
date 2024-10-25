using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class updatelib_playlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Library_Playlists_Playlists_LibraryId",
                table: "Library_Playlists");

            migrationBuilder.CreateIndex(
                name: "IX_Library_Playlists_PlaylistId",
                table: "Library_Playlists",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Playlists_Playlists_PlaylistId",
                table: "Library_Playlists",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "PlaylistId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Library_Playlists_Playlists_PlaylistId",
                table: "Library_Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Library_Playlists_PlaylistId",
                table: "Library_Playlists");

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Playlists_Playlists_LibraryId",
                table: "Library_Playlists",
                column: "LibraryId",
                principalTable: "Playlists",
                principalColumn: "PlaylistId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
