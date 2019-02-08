using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LayoutRecordField_LayoutsActRecords_LayoutRecordId",
                table: "LayoutRecordField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LayoutRecordField",
                table: "LayoutRecordField");

            migrationBuilder.RenameTable(
                name: "LayoutRecordField",
                newName: "LayoutsActRecordsFields");

            migrationBuilder.RenameIndex(
                name: "IX_LayoutRecordField_LayoutRecordId",
                table: "LayoutsActRecordsFields",
                newName: "IX_LayoutsActRecordsFields_LayoutRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LayoutsActRecordsFields",
                table: "LayoutsActRecordsFields",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LayoutsActRecordsFields_LayoutsActRecords_LayoutRecordId",
                table: "LayoutsActRecordsFields",
                column: "LayoutRecordId",
                principalTable: "LayoutsActRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LayoutsActRecordsFields_LayoutsActRecords_LayoutRecordId",
                table: "LayoutsActRecordsFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LayoutsActRecordsFields",
                table: "LayoutsActRecordsFields");

            migrationBuilder.RenameTable(
                name: "LayoutsActRecordsFields",
                newName: "LayoutRecordField");

            migrationBuilder.RenameIndex(
                name: "IX_LayoutsActRecordsFields_LayoutRecordId",
                table: "LayoutRecordField",
                newName: "IX_LayoutRecordField_LayoutRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LayoutRecordField",
                table: "LayoutRecordField",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LayoutRecordField_LayoutsActRecords_LayoutRecordId",
                table: "LayoutRecordField",
                column: "LayoutRecordId",
                principalTable: "LayoutsActRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
