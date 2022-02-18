using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class projectrelation1context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileDownload_Projects_ProjectsID",
                table: "FileDownload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileDownload",
                table: "FileDownload");

            migrationBuilder.RenameTable(
                name: "FileDownload",
                newName: "FileDownloads");

            migrationBuilder.RenameIndex(
                name: "IX_FileDownload_ProjectsID",
                table: "FileDownloads",
                newName: "IX_FileDownloads_ProjectsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileDownloads",
                table: "FileDownloads",
                column: "FileID");

            migrationBuilder.AddForeignKey(
                name: "FK_FileDownloads_Projects_ProjectsID",
                table: "FileDownloads",
                column: "ProjectsID",
                principalTable: "Projects",
                principalColumn: "ProjectsID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileDownloads_Projects_ProjectsID",
                table: "FileDownloads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileDownloads",
                table: "FileDownloads");

            migrationBuilder.RenameTable(
                name: "FileDownloads",
                newName: "FileDownload");

            migrationBuilder.RenameIndex(
                name: "IX_FileDownloads_ProjectsID",
                table: "FileDownload",
                newName: "IX_FileDownload_ProjectsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileDownload",
                table: "FileDownload",
                column: "FileID");

            migrationBuilder.AddForeignKey(
                name: "FK_FileDownload_Projects_ProjectsID",
                table: "FileDownload",
                column: "ProjectsID",
                principalTable: "Projects",
                principalColumn: "ProjectsID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
