using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class Token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Apartments",
                type: "nvarchar(64)",   // 👈 WAŻNE
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Token",
                table: "Apartments",
                column: "Token",
                unique: true);
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apartments_Token",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Apartments");
        }

    }
}
