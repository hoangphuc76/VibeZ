using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class udUPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_U_packages",
                table: "U_packages");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "U_packages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_U_packages",
                table: "U_packages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_U_packages_PackageId",
                table: "U_packages",
                column: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_U_packages",
                table: "U_packages");

            migrationBuilder.DropIndex(
                name: "IX_U_packages_PackageId",
                table: "U_packages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "U_packages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_U_packages",
                table: "U_packages",
                columns: new[] { "PackageId", "UserId" });
        }
    }
}
