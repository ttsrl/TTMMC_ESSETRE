using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LayoutId",
                table: "LayoutsRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LayoutsRecords_LayoutId",
                table: "LayoutsRecords",
                column: "LayoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_LayoutsRecords_Layouts_LayoutId",
                table: "LayoutsRecords",
                column: "LayoutId",
                principalTable: "Layouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LayoutsRecords_Layouts_LayoutId",
                table: "LayoutsRecords");

            migrationBuilder.DropIndex(
                name: "IX_LayoutsRecords_LayoutId",
                table: "LayoutsRecords");

            migrationBuilder.DropColumn(
                name: "LayoutId",
                table: "LayoutsRecords");
        }
    }
}
