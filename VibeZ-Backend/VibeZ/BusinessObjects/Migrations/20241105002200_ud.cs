using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class ud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Tracks",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));

            migrationBuilder.AddColumn<bool>(
                name: "PendingApproval",
                table: "Tracks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "TrackPlayLists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Libraries",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 5),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 4));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PendingApproval",
                table: "Tracks");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Tracks",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "TrackPlayLists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Library_Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Libraries",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Artists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDate",
                table: "Albums",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 11, 4),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 11, 5));
        }
    }
}
