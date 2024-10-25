using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLib_Artist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Library_Album_Albums_AlbumId",
                table: "Library_Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Library_Album_Libraries_LibraryId",
                table: "Library_Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Library_Playlist_Libraries_LibraryId",
                table: "Library_Playlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Library_Playlist_Playlists_PlaylistId",
                table: "Library_Playlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Library_Playlist",
                table: "Library_Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Library_Playlist_PlaylistId",
                table: "Library_Playlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Library_Album",
                table: "Library_Album");

            migrationBuilder.RenameTable(
                name: "Library_Playlist",
                newName: "Library_Playlists");

            migrationBuilder.RenameTable(
                name: "Library_Album",
                newName: "Library_Albums");

            migrationBuilder.RenameIndex(
                name: "IX_Library_Album_AlbumId",
                table: "Library_Albums",
                newName: "IX_Library_Albums_AlbumId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Libraries",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 10, 19),
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Libraries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 10, 19),
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 10, 19),
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library_Playlists",
                table: "Library_Playlists",
                columns: new[] { "LibraryId", "PlaylistId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library_Albums",
                table: "Library_Albums",
                columns: new[] { "LibraryId", "AlbumId" });

            migrationBuilder.CreateTable(
                name: "Library_Artists",
                columns: table => new
                {
                    LibraryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValue: new DateOnly(2024, 10, 19)),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library_Artists", x => new { x.LibraryId, x.ArtistId });
                    table.ForeignKey(
                        name: "FK_Library_Artists_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Library_Artists_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Library_Artists_ArtistId",
                table: "Library_Artists",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Albums_Albums_AlbumId",
                table: "Library_Albums",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Albums_Libraries_LibraryId",
                table: "Library_Albums",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Playlists_Libraries_LibraryId",
                table: "Library_Playlists",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Playlists_Playlists_LibraryId",
                table: "Library_Playlists",
                column: "LibraryId",
                principalTable: "Playlists",
                principalColumn: "PlaylistId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Library_Albums_Albums_AlbumId",
                table: "Library_Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Library_Albums_Libraries_LibraryId",
                table: "Library_Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Library_Playlists_Libraries_LibraryId",
                table: "Library_Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Library_Playlists_Playlists_LibraryId",
                table: "Library_Playlists");

            migrationBuilder.DropTable(
                name: "Library_Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Library_Playlists",
                table: "Library_Playlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Library_Albums",
                table: "Library_Albums");

            migrationBuilder.RenameTable(
                name: "Library_Playlists",
                newName: "Library_Playlist");

            migrationBuilder.RenameTable(
                name: "Library_Albums",
                newName: "Library_Album");

            migrationBuilder.RenameIndex(
                name: "IX_Library_Albums_AlbumId",
                table: "Library_Album",
                newName: "IX_Library_Album_AlbumId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Libraries",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 10, 19));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Libraries",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Playlist",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 10, 19));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Album",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 10, 19));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library_Playlist",
                table: "Library_Playlist",
                columns: new[] { "LibraryId", "PlaylistId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library_Album",
                table: "Library_Album",
                columns: new[] { "LibraryId", "AlbumId" });

            migrationBuilder.CreateIndex(
                name: "IX_Library_Playlist_PlaylistId",
                table: "Library_Playlist",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Album_Albums_AlbumId",
                table: "Library_Album",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Album_Libraries_LibraryId",
                table: "Library_Album",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Playlist_Libraries_LibraryId",
                table: "Library_Playlist",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Playlist_Playlists_PlaylistId",
                table: "Library_Playlist",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "PlaylistId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
