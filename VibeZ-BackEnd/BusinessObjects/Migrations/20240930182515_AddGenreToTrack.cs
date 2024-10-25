using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddGenreToTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 18, 25, 13, 58, DateTimeKind.Utc).AddTicks(6039),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 11, 28, 13, 659, DateTimeKind.Utc).AddTicks(3828));

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Tracks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 18, 25, 13, 58, DateTimeKind.Utc).AddTicks(2488),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 11, 28, 13, 658, DateTimeKind.Utc).AddTicks(9702));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Tracks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 11, 28, 13, 659, DateTimeKind.Utc).AddTicks(3828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 18, 25, 13, 58, DateTimeKind.Utc).AddTicks(6039));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 11, 28, 13, 658, DateTimeKind.Utc).AddTicks(9702),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 18, 25, 13, 58, DateTimeKind.Utc).AddTicks(2488));
        }
    }
}
