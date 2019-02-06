using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LayoutsRecords_Layouts_LayoutId",
                table: "LayoutsRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LayoutsRecords",
                table: "LayoutsRecords");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "LayoutsRecords");

            migrationBuilder.RenameTable(
                name: "LayoutsRecords",
                newName: "LayoutsActRecords");

            migrationBuilder.RenameIndex(
                name: "IX_LayoutsRecords_LayoutId",
                table: "LayoutsActRecords",
                newName: "IX_LayoutsActRecords_LayoutId");

            migrationBuilder.AddColumn<int>(
                name: "LayoutSetRecordId",
                table: "Layouts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LayoutsActRecords",
                table: "LayoutsActRecords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LayoutRecordField",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    LayoutRecordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutRecordField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LayoutRecordField_LayoutsActRecords_LayoutRecordId",
                        column: x => x.LayoutRecordId,
                        principalTable: "LayoutsActRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_LayoutSetRecordId",
                table: "Layouts",
                column: "LayoutSetRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutRecordField_LayoutRecordId",
                table: "LayoutRecordField",
                column: "LayoutRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Layouts_LayoutsActRecords_LayoutSetRecordId",
                table: "Layouts",
                column: "LayoutSetRecordId",
                principalTable: "LayoutsActRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LayoutsActRecords_Layouts_LayoutId",
                table: "LayoutsActRecords",
                column: "LayoutId",
                principalTable: "Layouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Layouts_LayoutsActRecords_LayoutSetRecordId",
                table: "Layouts");

            migrationBuilder.DropForeignKey(
                name: "FK_LayoutsActRecords_Layouts_LayoutId",
                table: "LayoutsActRecords");

            migrationBuilder.DropTable(
                name: "LayoutRecordField");

            migrationBuilder.DropIndex(
                name: "IX_Layouts_LayoutSetRecordId",
                table: "Layouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LayoutsActRecords",
                table: "LayoutsActRecords");

            migrationBuilder.DropColumn(
                name: "LayoutSetRecordId",
                table: "Layouts");

            migrationBuilder.RenameTable(
                name: "LayoutsActRecords",
                newName: "LayoutsRecords");

            migrationBuilder.RenameIndex(
                name: "IX_LayoutsActRecords_LayoutId",
                table: "LayoutsRecords",
                newName: "IX_LayoutsRecords_LayoutId");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "LayoutsRecords",
                nullable: true);

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
    }
}
