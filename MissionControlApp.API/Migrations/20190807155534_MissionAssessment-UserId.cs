using Microsoft.EntityFrameworkCore.Migrations;

namespace MissionControlApp.API.Migrations
{
    public partial class MissionAssessmentUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MissionAssessments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MissionAssessments_UserId",
                table: "MissionAssessments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionAssessments_AspNetUsers_UserId",
                table: "MissionAssessments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionAssessments_AspNetUsers_UserId",
                table: "MissionAssessments");

            migrationBuilder.DropIndex(
                name: "IX_MissionAssessments_UserId",
                table: "MissionAssessments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MissionAssessments");
        }
    }
}
