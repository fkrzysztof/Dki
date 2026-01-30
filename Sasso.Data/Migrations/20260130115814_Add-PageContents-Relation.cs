using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class AddPageContentsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentID",
                table: "PageContents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageContents_ApartmentID",
                table: "PageContents",
                column: "ApartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_Apartments_ApartmentID",
                table: "PageContents",
                column: "ApartmentID",
                principalTable: "Apartments",
                principalColumn: "ApartmentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_Apartments_ApartmentID",
                table: "PageContents");

            migrationBuilder.DropIndex(
                name: "IX_PageContents_ApartmentID",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "ApartmentID",
                table: "PageContents");
        }
    }
}
