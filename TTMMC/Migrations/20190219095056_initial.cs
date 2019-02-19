using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC_ESSETRE.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LayoutsActRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LayoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutsActRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Layouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MachineName = table.Column<string>(nullable: true),
                    MachineNumber = table.Column<int>(nullable: true),
                    Machine = table.Column<int>(nullable: false),
                    LayoutType = table.Column<string>(nullable: true),
                    LayoutNumber = table.Column<long>(nullable: false),
                    LayoutPhase = table.Column<int>(nullable: true),
                    ItemCode = table.Column<int>(nullable: true),
                    ItemDescription = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Meters = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LayoutSetRecordId = table.Column<int>(nullable: true),
                    StartTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Layouts_LayoutsActRecords_LayoutSetRecordId",
                        column: x => x.LayoutSetRecordId,
                        principalTable: "LayoutsActRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LayoutsActRecordsFields",
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
                    table.PrimaryKey("PK_LayoutsActRecordsFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LayoutsActRecordsFields_LayoutsActRecords_LayoutRecordId",
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
                name: "IX_LayoutsActRecords_LayoutId",
                table: "LayoutsActRecords",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutsActRecordsFields_LayoutRecordId",
                table: "LayoutsActRecordsFields",
                column: "LayoutRecordId");

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

            migrationBuilder.DropTable(
                name: "LayoutsActRecordsFields");

            migrationBuilder.DropTable(
                name: "LayoutsActRecords");

            migrationBuilder.DropTable(
                name: "Layouts");
        }
    }
}
