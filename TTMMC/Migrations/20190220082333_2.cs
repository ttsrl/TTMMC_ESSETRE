using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC_ESSETRE.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTimestamp",
                table: "Recipes",
                newName: "Timestamp");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Recipes",
                newName: "StartTimestamp");
        }
    }
}
