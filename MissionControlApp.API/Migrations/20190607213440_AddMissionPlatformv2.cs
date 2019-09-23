using Microsoft.EntityFrameworkCore.Migrations;

namespace MissionControlApp.API.Migrations
{
    public partial class AddMissionPlatformv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionPlatform_Missions_MissionId",
                table: "MissionPlatform");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionPlatform_Platforms_PlatformId",
                table: "MissionPlatform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionPlatform",
                table: "MissionPlatform");

            migrationBuilder.RenameTable(
                name: "MissionPlatform",
                newName: "MissionPlatforms");

            migrationBuilder.RenameIndex(
                name: "IX_MissionPlatform_PlatformId",
                table: "MissionPlatforms",
                newName: "IX_MissionPlatforms_PlatformId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionPlatform_MissionId",
                table: "MissionPlatforms",
                newName: "IX_MissionPlatforms_MissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionPlatforms",
                table: "MissionPlatforms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionPlatforms_Missions_MissionId",
                table: "MissionPlatforms",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionPlatforms_Platforms_PlatformId",
                table: "MissionPlatforms",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionPlatforms_Missions_MissionId",
                table: "MissionPlatforms");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionPlatforms_Platforms_PlatformId",
                table: "MissionPlatforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionPlatforms",
                table: "MissionPlatforms");

            migrationBuilder.RenameTable(
                name: "MissionPlatforms",
                newName: "MissionPlatform");

            migrationBuilder.RenameIndex(
                name: "IX_MissionPlatforms_PlatformId",
                table: "MissionPlatform",
                newName: "IX_MissionPlatform_PlatformId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionPlatforms_MissionId",
                table: "MissionPlatform",
                newName: "IX_MissionPlatform_MissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionPlatform",
                table: "MissionPlatform",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionPlatform_Missions_MissionId",
                table: "MissionPlatform",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionPlatform_Platforms_PlatformId",
                table: "MissionPlatform",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
