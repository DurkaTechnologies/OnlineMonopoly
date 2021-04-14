using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddLevelsToRentSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstCoef",
                table: "RentSettings");

            migrationBuilder.RenameColumn(
                name: "StartCost",
                table: "RentSettings",
                newName: "ThirdLevel");

            migrationBuilder.RenameColumn(
                name: "SecondCoef",
                table: "RentSettings",
                newName: "StartRent");

            migrationBuilder.AddColumn<int>(
                name: "FifthLevel",
                table: "RentSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirstLevel",
                table: "RentSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FourthLevel",
                table: "RentSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondLevel",
                table: "RentSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FifthLevel",
                table: "RentSettings");

            migrationBuilder.DropColumn(
                name: "FirstLevel",
                table: "RentSettings");

            migrationBuilder.DropColumn(
                name: "FourthLevel",
                table: "RentSettings");

            migrationBuilder.DropColumn(
                name: "SecondLevel",
                table: "RentSettings");

            migrationBuilder.RenameColumn(
                name: "ThirdLevel",
                table: "RentSettings",
                newName: "StartCost");

            migrationBuilder.RenameColumn(
                name: "StartRent",
                table: "RentSettings",
                newName: "SecondCoef");

            migrationBuilder.AddColumn<float>(
                name: "FirstCoef",
                table: "RentSettings",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
