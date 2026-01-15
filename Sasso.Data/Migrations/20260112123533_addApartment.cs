using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class AddApartmentSafe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Dodaj kolumnę ApartmentID jako nullable (bez wymuszania defaultValue)
            migrationBuilder.AddColumn<int>(
                name: "ApartmentID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            // 2. Utwórz tabelę Apartments
            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    ApartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pietro = table.Column<int>(type: "int", nullable: false),
                    LiczbaPieterWBudynku = table.Column<int>(type: "int", nullable: false),
                    Metraz = table.Column<double>(type: "float", nullable: false),
                    LiczbaPokoi = table.Column<int>(type: "int", nullable: false),
                    WcRazemZLazienka = table.Column<bool>(type: "bit", nullable: false),
                    Balkon = table.Column<bool>(type: "bit", nullable: false),
                    Winda = table.Column<bool>(type: "bit", nullable: false),
                    Piwnica = table.Column<bool>(type: "bit", nullable: false),
                    OgrzewaniePodlogowe = table.Column<bool>(type: "bit", nullable: false),
                    Klimatyzacja = table.Column<bool>(type: "bit", nullable: false),
                    Garaz = table.Column<bool>(type: "bit", nullable: false),
                    MiejsceParkingoweNaZewnatrz = table.Column<bool>(type: "bit", nullable: false),
                    Ogrod = table.Column<bool>(type: "bit", nullable: false),
                    Taras = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.ApartmentID);
                });

            // 3. Utwórz indeks dla kolumny ApartmentID w MyFiles
            migrationBuilder.CreateIndex(
                name: "IX_MyFiles_ApartmentID",
                table: "MyFiles",
                column: "ApartmentID");

            // 4. Dodaj klucz obcy
            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_Apartments_ApartmentID",
                table: "MyFiles",
                column: "ApartmentID",
                principalTable: "Apartments",
                principalColumn: "ApartmentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_Apartments_ApartmentID",
                table: "MyFiles");

            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_ApartmentID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "ApartmentID",
                table: "MyFiles");
        }
    }
}
