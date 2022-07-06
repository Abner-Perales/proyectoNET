using Microsoft.EntityFrameworkCore.Migrations;

namespace GymManager.DataAccess.Migrations
{
    public partial class checkTypedelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_CheckTypes_CheckTypeId",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Checks_CheckTypeId",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "CheckTypeId",
                table: "Checks");

            migrationBuilder.AddColumn<string>(
                name: "CheckType",
                table: "Checks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckType",
                table: "Checks");

            migrationBuilder.AddColumn<int>(
                name: "CheckTypeId",
                table: "Checks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checks_CheckTypeId",
                table: "Checks",
                column: "CheckTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_CheckTypes_CheckTypeId",
                table: "Checks",
                column: "CheckTypeId",
                principalTable: "CheckTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
