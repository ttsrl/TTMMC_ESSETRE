using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LayoutRecord_Layouts_LayoutId",
                table: "LayoutRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LayoutRecord",
                table: "LayoutRecord");

            migrationBuilder.RenameTable(
                name: "LayoutRecord",
                newName: "LayoutsRecords");

            migrationBuilder.RenameIndex(
                name: "IX_LayoutRecord_LayoutId",
                table: "LayoutsRecords",
                newName: "IX_LayoutsRecords_LayoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LayoutsRecords",
                table: "LayoutsRecords",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_LayoutsRecords",
                table: "LayoutsRecords");

            migrationBuilder.RenameTable(
                name: "LayoutsRecords",
                newName: "LayoutRecord");

            migrationBuilder.RenameIndex(
                name: "IX_LayoutsRecords_LayoutId",
                table: "LayoutRecord",
                newName: "IX_LayoutRecord_LayoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LayoutRecord",
                table: "LayoutRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LayoutRecord_Layouts_LayoutId",
                table: "LayoutRecord",
                column: "LayoutId",
                principalTable: "Layouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
