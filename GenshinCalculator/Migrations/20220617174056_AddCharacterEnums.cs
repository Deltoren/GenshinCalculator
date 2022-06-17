using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenshinCalculator.Migrations
{
    public partial class AddCharacterEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Vision",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeaponType",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vision",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "WeaponType",
                table: "Characters");
        }
    }
}
