using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class addfilesCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilesCollectionID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyFiles_FilesCollectionID",
                table: "MyFiles",
                column: "FilesCollectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_Projects_FilesCollectionID",
                table: "MyFiles",
                column: "FilesCollectionID",
                principalTable: "Projects",
                principalColumn: "ProjectsID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_Projects_FilesCollectionID",
                table: "MyFiles");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_FilesCollectionID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "FilesCollectionID",
                table: "MyFiles");
        }
    }
}
