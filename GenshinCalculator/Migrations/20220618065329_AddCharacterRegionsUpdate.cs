using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenshinCalculator.Migrations
{
    public partial class AddCharacterRegionsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterRegion_Characters_CharacterId",
                table: "CharacterRegion");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterRegion_Regions_RegionId",
                table: "CharacterRegion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterRegion",
                table: "CharacterRegion");

            migrationBuilder.RenameTable(
                name: "CharacterRegion",
                newName: "CharacterRegions");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterRegion_RegionId",
                table: "CharacterRegions",
                newName: "IX_CharacterRegions_RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterRegions",
                table: "CharacterRegions",
                columns: new[] { "CharacterId", "RegionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterRegions_Characters_CharacterId",
                table: "CharacterRegions",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterRegions_Regions_RegionId",
                table: "CharacterRegions",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterRegions_Characters_CharacterId",
                table: "CharacterRegions");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterRegions_Regions_RegionId",
                table: "CharacterRegions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterRegions",
                table: "CharacterRegions");

            migrationBuilder.RenameTable(
                name: "CharacterRegions",
                newName: "CharacterRegion");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterRegions_RegionId",
                table: "CharacterRegion",
                newName: "IX_CharacterRegion_RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterRegion",
                table: "CharacterRegion",
                columns: new[] { "CharacterId", "RegionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterRegion_Characters_CharacterId",
                table: "CharacterRegion",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterRegion_Regions_RegionId",
                table: "CharacterRegion",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
