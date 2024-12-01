using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class udArtistPending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Tracks",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "TrackPlayLists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Libraries",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 7));

            migrationBuilder.CreateTable(
                name: "ArtistPendings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArtistName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Song = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LyricLRC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgBackground = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistPendings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtistPendings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistPendings_UserId",
                table: "ArtistPendings",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistPendings");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Tracks",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "TrackPlayLists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Libraries",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 9));
        }
    }
}
