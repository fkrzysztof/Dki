using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class backgroundrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BackgroundListID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyFiles_BackgroundListID",
                table: "MyFiles",
                column: "BackgroundListID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_Settings_BackgroundListID",
                table: "MyFiles",
                column: "BackgroundListID",
                principalTable: "Settings",
                principalColumn: "SettingsID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_Settings_BackgroundListID",
                table: "MyFiles");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_BackgroundListID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "BackgroundListID",
                table: "MyFiles");
        }
    }
}
