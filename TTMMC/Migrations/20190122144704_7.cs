using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MixtureItems_Layouts_LayoutId",
                table: "MixtureItems");

            migrationBuilder.DropIndex(
                name: "IX_MixtureItems_LayoutId",
                table: "MixtureItems");

            migrationBuilder.DropColumn(
                name: "LayoutId",
                table: "MixtureItems");

            migrationBuilder.AddColumn<int>(
                name: "MixtureId",
                table: "Layouts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_MixtureId",
                table: "Layouts",
                column: "MixtureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Layouts_Mixtures_MixtureId",
                table: "Layouts",
                column: "MixtureId",
                principalTable: "Mixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Layouts_Mixtures_MixtureId",
                table: "Layouts");

            migrationBuilder.DropIndex(
                name: "IX_Layouts_MixtureId",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "MixtureId",
                table: "Layouts");

            migrationBuilder.AddColumn<int>(
                name: "LayoutId",
                table: "MixtureItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MixtureItems_LayoutId",
                table: "MixtureItems",
                column: "LayoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_MixtureItems_Layouts_LayoutId",
                table: "MixtureItems",
                column: "LayoutId",
                principalTable: "Layouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
