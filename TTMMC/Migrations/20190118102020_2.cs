using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Moulds_MouldId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Moulds_Clients_ClientId",
                table: "Moulds");

            migrationBuilder.DropForeignKey(
                name: "FK_Moulds_Locations_LocationId",
                table: "Moulds");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Moulds_ClientId",
                table: "Moulds");

            migrationBuilder.DropIndex(
                name: "IX_Moulds_LocationId",
                table: "Moulds");

            migrationBuilder.DropIndex(
                name: "IX_Materials_MouldId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Moulds");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Moulds");

            migrationBuilder.DropColumn(
                name: "Machine",
                table: "Moulds");

            migrationBuilder.DropColumn(
                name: "MouldId",
                table: "Materials");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Moulds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Moulds");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Moulds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Moulds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Machine",
                table: "Moulds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MouldId",
                table: "Materials",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    X = table.Column<string>(nullable: true),
                    Y = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moulds_ClientId",
                table: "Moulds",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Moulds_LocationId",
                table: "Moulds",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MouldId",
                table: "Materials",
                column: "MouldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Moulds_MouldId",
                table: "Materials",
                column: "MouldId",
                principalTable: "Moulds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Moulds_Clients_ClientId",
                table: "Moulds",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Moulds_Locations_LocationId",
                table: "Moulds",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
