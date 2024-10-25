using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAlbum2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 2, 15, 4, 6, 275, DateTimeKind.Utc).AddTicks(9039),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 2, 14, 41, 44, 266, DateTimeKind.Utc).AddTicks(5649));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 2, 15, 4, 6, 275, DateTimeKind.Utc).AddTicks(6531),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 2, 14, 41, 44, 266, DateTimeKind.Utc).AddTicks(3313));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfRelease",
                table: "Albums",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Albums",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 2, 15, 4, 6, 276, DateTimeKind.Utc).AddTicks(4353),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 2, 14, 41, 44, 267, DateTimeKind.Utc).AddTicks(847));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 2, 14, 41, 44, 266, DateTimeKind.Utc).AddTicks(5649),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 2, 15, 4, 6, 275, DateTimeKind.Utc).AddTicks(9039));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 2, 14, 41, 44, 266, DateTimeKind.Utc).AddTicks(3313),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 2, 15, 4, 6, 275, DateTimeKind.Utc).AddTicks(6531));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfRelease",
                table: "Albums",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Albums",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 2, 14, 41, 44, 267, DateTimeKind.Utc).AddTicks(847),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 2, 15, 4, 6, 276, DateTimeKind.Utc).AddTicks(4353));
        }
    }
}
