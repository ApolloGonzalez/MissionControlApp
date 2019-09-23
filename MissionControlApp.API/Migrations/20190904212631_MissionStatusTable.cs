using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MissionControlApp.API.Migrations
{
    public partial class MissionStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MissionStatusName = table.Column<string>(nullable: true),
                    MissionStatusCode = table.Column<string>(nullable: true),
                    MissionStatusAlias = table.Column<string>(nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MissionStatusType = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionStatus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionStatus");
        }
    }
}
