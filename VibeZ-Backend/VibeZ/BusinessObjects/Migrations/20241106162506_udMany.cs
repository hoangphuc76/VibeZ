using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class udMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Tracks",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "TrackPlayLists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Libraries",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AddColumn<bool>(
                name: "IsFollow",
                table: "Follows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "Artists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.CreateTable(
                name: "TrackListeners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listener = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackListeners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackListeners_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_userId",
                table: "Artists",
                column: "userId",
                unique: true,
                filter: "[userId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrackListeners_TrackId",
                table: "TrackListeners",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_userId",
                table: "Artists",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Users_userId",
                table: "Artists");

            migrationBuilder.DropTable(
                name: "TrackListeners");

            migrationBuilder.DropIndex(
                name: "IX_Artists_userId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "IsFollow",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Artists");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Tracks",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "TrackPlayLists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Libraries",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 6));
        }
    }
}
