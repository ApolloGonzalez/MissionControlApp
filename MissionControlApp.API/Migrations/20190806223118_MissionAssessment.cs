using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MissionControlApp.API.Migrations
{
    public partial class MissionAssessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionAssessments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MissionId = table.Column<int>(nullable: false),
                    DataLocation = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    DataDomainExpert = table.Column<string>(nullable: true),
                    ChallengeSolution = table.Column<string>(nullable: true),
                    DataQuality = table.Column<string>(nullable: true),
                    ChallengeType = table.Column<string>(nullable: true),
                    InfrastructureRequirement = table.Column<string>(nullable: true),
                    AccuracyRequirement = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    UpdateDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionAssessments_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionAssessments_MissionId",
                table: "MissionAssessments",
                column: "MissionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionAssessments");
        }
    }
}
