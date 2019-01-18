using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Moulds_MouldId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_MouldId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "MouldId",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Moulds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moulds_LocationId",
                table: "Moulds",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moulds_Locations_LocationId",
                table: "Moulds",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moulds_Locations_LocationId",
                table: "Moulds");

            migrationBuilder.DropIndex(
                name: "IX_Moulds_LocationId",
                table: "Moulds");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Moulds");

            migrationBuilder.AddColumn<int>(
                name: "MouldId",
                table: "Locations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_MouldId",
                table: "Locations",
                column: "MouldId",
                unique: true,
                filter: "[MouldId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Moulds_MouldId",
                table: "Locations",
                column: "MouldId",
                principalTable: "Moulds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
