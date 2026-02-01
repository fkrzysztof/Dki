using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class AddPdfFilepagecontent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PdfFileId",
                table: "PageContents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageContents_PdfFileId",
                table: "PageContents",
                column: "PdfFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_MyFiles_PdfFileId",
                table: "PageContents",
                column: "PdfFileId",
                principalTable: "MyFiles",
                principalColumn: "FileID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_MyFiles_PdfFileId",
                table: "PageContents");

            migrationBuilder.DropIndex(
                name: "IX_PageContents_PdfFileId",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "PdfFileId",
                table: "PageContents");
        }
    }
}
