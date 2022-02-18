using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class projectrelation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaItem",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileDownload",
                columns: table => new
                {
                    FileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDownload", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_FileDownload_Projects_ProjectsID",
                        column: x => x.ProjectsID,
                        principalTable: "Projects",
                        principalColumn: "ProjectsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileDownload_ProjectsID",
                table: "FileDownload",
                column: "ProjectsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileDownload");

            migrationBuilder.DropColumn(
                name: "MediaItem",
                table: "Projects");
        }
    }
}
