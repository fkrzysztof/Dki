using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class Apartmentstextinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KodPocztowy",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kraj",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Miasto",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nazwa",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumerBudynku",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumerMieszkania",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon1",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon2",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ulica",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "KodPocztowy",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Kraj",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Miasto",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Nazwa",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "NumerBudynku",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "NumerMieszkania",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Opis",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Telefon1",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Telefon2",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Ulica",
                table: "Apartments");
        }
    }
}
