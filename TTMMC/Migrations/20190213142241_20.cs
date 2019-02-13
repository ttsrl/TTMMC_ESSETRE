using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultMasterId",
                table: "Moulds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moulds_DefaultMasterId",
                table: "Moulds",
                column: "DefaultMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moulds_Masters_DefaultMasterId",
                table: "Moulds",
                column: "DefaultMasterId",
                principalTable: "Masters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moulds_Masters_DefaultMasterId",
                table: "Moulds");

            migrationBuilder.DropIndex(
                name: "IX_Moulds_DefaultMasterId",
                table: "Moulds");

            migrationBuilder.DropColumn(
                name: "DefaultMasterId",
                table: "Moulds");
        }
    }
}
