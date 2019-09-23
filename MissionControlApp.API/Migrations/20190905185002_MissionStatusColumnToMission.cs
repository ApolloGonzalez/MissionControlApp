using Microsoft.EntityFrameworkCore.Migrations;

namespace MissionControlApp.API.Migrations
{
    public partial class MissionStatusColumnToMission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MissionStatusId",
                table: "Missions",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Missions_MissionStatusId",
                table: "Missions",
                column: "MissionStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_MissionStatus_MissionStatusId",
                table: "Missions",
                column: "MissionStatusId",
                principalTable: "MissionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_MissionStatus_MissionStatusId",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Missions_MissionStatusId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "MissionStatusId",
                table: "Missions");
        }
    }
}
