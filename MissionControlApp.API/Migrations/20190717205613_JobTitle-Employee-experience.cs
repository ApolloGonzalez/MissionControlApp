using Microsoft.EntityFrameworkCore.Migrations;

namespace MissionControlApp.API.Migrations
{
    public partial class JobTitleEmployeeexperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LookingFor",
                table: "AspNetUsers",
                newName: "JobTitle");

            migrationBuilder.AddColumn<string>(
                name: "BusinessImpact",
                table: "Missions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Employee",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessImpact",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Employee",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "JobTitle",
                table: "AspNetUsers",
                newName: "LookingFor");
        }
    }
}
