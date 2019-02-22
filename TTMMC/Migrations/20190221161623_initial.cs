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
                name: "LayoutsRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    LayoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutsRecords", x => x.Id);
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
                        name: "FK_Layouts_LayoutsRecords_LayoutSetRecordId",
                        column: x => x.LayoutSetRecordId,
                        principalTable: "LayoutsRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LayoutsRecordsFields",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    LayoutRecordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutsRecordsFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LayoutsRecordsFields_LayoutsRecords_LayoutRecordId",
                        column: x => x.LayoutRecordId,
                        principalTable: "LayoutsRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    RepiceSettingsId = table.Column<int>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_LayoutsRecords_RepiceSettingsId",
                        column: x => x.RepiceSettingsId,
                        principalTable: "LayoutsRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_LayoutSetRecordId",
                table: "Layouts",
                column: "LayoutSetRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutsRecords_LayoutId",
                table: "LayoutsRecords",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutsRecordsFields_LayoutRecordId",
                table: "LayoutsRecordsFields",
                column: "LayoutRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RepiceSettingsId",
                table: "Recipes",
                column: "RepiceSettingsId");

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
                name: "FK_Layouts_LayoutsRecords_LayoutSetRecordId",
                table: "Layouts");

            migrationBuilder.DropTable(
                name: "LayoutsRecordsFields");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "LayoutsRecords");

            migrationBuilder.DropTable(
                name: "Layouts");
        }
    }
}
