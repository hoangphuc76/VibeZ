using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class udddD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Users_userId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_userId",
                table: "Artists");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Artists",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UserId",
                table: "Artists",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_UserId",
                table: "Artists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Users_UserId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_UserId",
                table: "Artists");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Artists",
                newName: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_userId",
                table: "Artists",
                column: "userId",
                unique: true,
                filter: "[userId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_userId",
                table: "Artists",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
