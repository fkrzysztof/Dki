using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class projectrelation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaItem",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectsID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyFiles_ProjectsID",
                table: "MyFiles",
                column: "ProjectsID",
                unique: true,
                filter: "[ProjectsID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_Projects_ProjectsID",
                table: "MyFiles",
                column: "ProjectsID",
                principalTable: "Projects",
                principalColumn: "ProjectsID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_Projects_ProjectsID",
                table: "MyFiles");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_ProjectsID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "ProjectsID",
                table: "MyFiles");

            migrationBuilder.AddColumn<string>(
                name: "MediaItem",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
