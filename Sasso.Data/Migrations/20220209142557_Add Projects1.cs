using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class AddProjects1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    News = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Participants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormOfSupport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recruitment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfPublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndProject = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartProject = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectsID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
