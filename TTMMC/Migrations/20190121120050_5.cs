using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMC.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MixtureItems_Moulds_MouldId",
                table: "MixtureItems");

            migrationBuilder.RenameColumn(
                name: "MouldId",
                table: "MixtureItems",
                newName: "MixtureId");

            migrationBuilder.RenameIndex(
                name: "IX_MixtureItems_MouldId",
                table: "MixtureItems",
                newName: "IX_MixtureItems_MixtureId");

            migrationBuilder.AddColumn<int>(
                name: "DefaultMixtureId",
                table: "Moulds",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Mixtures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mixtures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moulds_DefaultMixtureId",
                table: "Moulds",
                column: "DefaultMixtureId");

            migrationBuilder.AddForeignKey(
                name: "FK_MixtureItems_Mixtures_MixtureId",
                table: "MixtureItems",
                column: "MixtureId",
                principalTable: "Mixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Moulds_Mixtures_DefaultMixtureId",
                table: "Moulds",
                column: "DefaultMixtureId",
                principalTable: "Mixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MixtureItems_Mixtures_MixtureId",
                table: "MixtureItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Moulds_Mixtures_DefaultMixtureId",
                table: "Moulds");

            migrationBuilder.DropTable(
                name: "Mixtures");

            migrationBuilder.DropIndex(
                name: "IX_Moulds_DefaultMixtureId",
                table: "Moulds");

            migrationBuilder.DropColumn(
                name: "DefaultMixtureId",
                table: "Moulds");

            migrationBuilder.RenameColumn(
                name: "MixtureId",
                table: "MixtureItems",
                newName: "MouldId");

            migrationBuilder.RenameIndex(
                name: "IX_MixtureItems_MixtureId",
                table: "MixtureItems",
                newName: "IX_MixtureItems_MouldId");

            migrationBuilder.AddForeignKey(
                name: "FK_MixtureItems_Moulds_MouldId",
                table: "MixtureItems",
                column: "MouldId",
                principalTable: "Moulds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
