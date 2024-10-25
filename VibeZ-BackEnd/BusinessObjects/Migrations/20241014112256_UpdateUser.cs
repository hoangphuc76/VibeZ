using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries");

            migrationBuilder.RenameColumn(
                name: "premium",
                table: "Users",
                newName: "Premium");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DOB",
                table: "Users",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 14, 11, 22, 54, 46, DateTimeKind.Utc).AddTicks(7902),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 4, 2, 29, 13, 31, DateTimeKind.Utc).AddTicks(765));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 14, 11, 22, 54, 46, DateTimeKind.Utc).AddTicks(4503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 4, 2, 29, 13, 30, DateTimeKind.Utc).AddTicks(8187));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Artists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 14, 11, 22, 54, 47, DateTimeKind.Utc).AddTicks(5019),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 4, 2, 29, 13, 31, DateTimeKind.Utc).AddTicks(8656));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Albums",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 14, 11, 22, 54, 47, DateTimeKind.Utc).AddTicks(2983),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 4, 2, 29, 13, 31, DateTimeKind.Utc).AddTicks(6331));

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Premium",
                table: "Users",
                newName: "premium");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 4, 2, 29, 13, 31, DateTimeKind.Utc).AddTicks(765),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 14, 11, 22, 54, 46, DateTimeKind.Utc).AddTicks(7902));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 4, 2, 29, 13, 30, DateTimeKind.Utc).AddTicks(8187),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 14, 11, 22, 54, 46, DateTimeKind.Utc).AddTicks(4503));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Artists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 4, 2, 29, 13, 31, DateTimeKind.Utc).AddTicks(8656),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 14, 11, 22, 54, 47, DateTimeKind.Utc).AddTicks(5019));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Albums",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 4, 2, 29, 13, 31, DateTimeKind.Utc).AddTicks(6331),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 14, 11, 22, 54, 47, DateTimeKind.Utc).AddTicks(2983));

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries",
                column: "UserId");
        }
    }
}
