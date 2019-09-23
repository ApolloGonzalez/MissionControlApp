using Microsoft.EntityFrameworkCore.Migrations;

namespace MissionControlApp.API.Migrations
{
    public partial class MissionCostAndROI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualCost",
                table: "Missions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualRoi",
                table: "Missions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedRoi",
                table: "Missions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedCost",
                table: "MissionAssessments",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualCost",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "ActualRoi",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "EstimatedRoi",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "EstimatedCost",
                table: "MissionAssessments");
        }
    }
}
