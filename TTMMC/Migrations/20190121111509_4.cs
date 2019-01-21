using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moulds_Clients_ClientId",
                table: "Moulds");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Moulds",
                newName: "DefaultClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Moulds_ClientId",
                table: "Moulds",
                newName: "IX_Moulds_DefaultClientId");

            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    ColorArgb = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Layouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: true),
                    MouldId = table.Column<int>(nullable: true),
                    Machine = table.Column<int>(nullable: false),
                    MasterId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Minced = table.Column<string>(nullable: true),
                    Umidification = table.Column<TimeSpan>(nullable: false),
                    Packaging = table.Column<int>(nullable: false),
                    PackagingQuantity = table.Column<int>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Layouts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Layouts_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Layouts_Moulds_MouldId",
                        column: x => x.MouldId,
                        principalTable: "Moulds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MixtureItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    MaterialId = table.Column<int>(nullable: true),
                    LayoutId = table.Column<int>(nullable: true),
                    MouldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MixtureItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MixtureItems_Layouts_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "Layouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MixtureItems_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MixtureItems_Moulds_MouldId",
                        column: x => x.MouldId,
                        principalTable: "Moulds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_ClientId",
                table: "Layouts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_MasterId",
                table: "Layouts",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_MouldId",
                table: "Layouts",
                column: "MouldId");

            migrationBuilder.CreateIndex(
                name: "IX_MixtureItems_LayoutId",
                table: "MixtureItems",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_MixtureItems_MaterialId",
                table: "MixtureItems",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MixtureItems_MouldId",
                table: "MixtureItems",
                column: "MouldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moulds_Clients_DefaultClientId",
                table: "Moulds",
                column: "DefaultClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moulds_Clients_DefaultClientId",
                table: "Moulds");

            migrationBuilder.DropTable(
                name: "MixtureItems");

            migrationBuilder.DropTable(
                name: "Layouts");

            migrationBuilder.DropTable(
                name: "Masters");

            migrationBuilder.RenameColumn(
                name: "DefaultClientId",
                table: "Moulds",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Moulds_DefaultClientId",
                table: "Moulds",
                newName: "IX_Moulds_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moulds_Clients_ClientId",
                table: "Moulds",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
