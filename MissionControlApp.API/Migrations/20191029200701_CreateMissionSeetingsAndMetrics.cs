using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MissionControlApp.API.Migrations
{
    public partial class CreateMissionSeetingsAndMetrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionAppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MissionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionAppSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionAppSettings_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissionMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MissionId = table.Column<int>(nullable: false),
                    Mission = table.Column<int>(nullable: false),
                    Models = table.Column<int>(nullable: false),
                    Ideas = table.Column<int>(nullable: false),
                    ModelsDeployed = table.Column<int>(nullable: false),
                    Experiments = table.Column<int>(nullable: false),
                    ExperimentRuns = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(type: "Money", nullable: false),
                    MissionStatus = table.Column<string>(nullable: true),
                    NotebookVMs = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    UpdateDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionMetrics_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionAppSettings_MissionId",
                table: "MissionAppSettings",
                column: "MissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MissionMetrics_MissionId",
                table: "MissionMetrics",
                column: "MissionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionAppSettings");

            migrationBuilder.DropTable(
                name: "MissionMetrics");
        }
    }
}
