using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Master",
                table: "Moulds");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Moulds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moulds_ClientId",
                table: "Moulds",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moulds_Clients_ClientId",
                table: "Moulds",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moulds_Clients_ClientId",
                table: "Moulds");

            migrationBuilder.DropIndex(
                name: "IX_Moulds_ClientId",
                table: "Moulds");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Moulds");

            migrationBuilder.AddColumn<string>(
                name: "Master",
                table: "Moulds",
                nullable: true);
        }
    }
}
