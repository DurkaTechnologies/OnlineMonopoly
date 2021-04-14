using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ShizoMigratin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_RentSettings_RentSettingId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "RentSettingsId",
                table: "Branches");

            migrationBuilder.AlterColumn<int>(
                name: "RentSettingId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_RentSettings_RentSettingId",
                table: "Branches",
                column: "RentSettingId",
                principalTable: "RentSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_RentSettings_RentSettingId",
                table: "Branches");

            migrationBuilder.AlterColumn<int>(
                name: "RentSettingId",
                table: "Branches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RentSettingsId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_RentSettings_RentSettingId",
                table: "Branches",
                column: "RentSettingId",
                principalTable: "RentSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
