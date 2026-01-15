//using Microsoft.EntityFrameworkCore.Migrations;

//namespace Sald.Data.Migrations
//{
//    public partial class AddApartmentFix : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.AddColumn<int>(
//                name: "ApartmentID",
//                table: "MyFiles",
//                type: "int",
//                nullable: true);

//            migrationBuilder.CreateTable(
//                name: "Apartments",
//                columns: table => new
//                {
//                    ApartmentID = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Pietro = table.Column<int>(type: "int", nullable: false),
//                    LiczbaPieterWBudynku = table.Column<int>(type: "int", nullable: false),
//                    Metraz = table.Column<double>(type: "float", nullable: false),
//                    LiczbaPokoi = table.Column<int>(type: "int", nullable: false),
//                    WcRazemZLazienka = table.Column<bool>(type: "bit", nullable: false),
//                    Balkon = table.Column<bool>(type: "bit", nullable: false),
//                    Winda = table.Column<bool>(type: "bit", nullable: false),
//                    Piwnica = table.Column<bool>(type: "bit", nullable: false),
//                    OgrzewaniePodlogowe = table.Column<bool>(type: "bit", nullable: false),
//                    Klimatyzacja = table.Column<bool>(type: "bit", nullable: false),
//                    Garaz = table.Column<bool>(type: "bit", nullable: false),
//                    MiejsceParkingoweNaZewnatrz = table.Column<bool>(type: "bit", nullable: false),
//                    Ogród = table.Column<bool>(type: "bit", nullable: false),
//                    Taras = table.Column<bool>(type: "bit", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Apartments", x => x.ApartmentID);
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_MyFiles_ApartmentID",
//                table: "MyFiles",
//                column: "ApartmentID");

//            migrationBuilder.AddForeignKey(
//                name: "FK_MyFiles_Apartments_ApartmentID",
//                table: "MyFiles",
//                column: "ApartmentID",
//                principalTable: "Apartments",
//                principalColumn: "ApartmentID",
//                onDelete: ReferentialAction.Restrict);
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_MyFiles_Apartments_ApartmentID",
//                table: "MyFiles");

//            migrationBuilder.DropTable(
//                name: "Apartments");

//            migrationBuilder.DropIndex(
//                name: "IX_MyFiles_ApartmentID",
//                table: "MyFiles");

//            migrationBuilder.DropColumn(
//                name: "ApartmentID",
//                table: "MyFiles");
//        }
//    }
//}


using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class AddApartmentFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Dodaj kolumnę ApartmentID tylko jeśli jej nie ma
            migrationBuilder.Sql(@"
                IF COL_LENGTH('MyFiles', 'ApartmentID') IS NULL
                BEGIN
                    ALTER TABLE MyFiles ADD ApartmentID INT NULL;
                END
            ");

            // 2. Utwórz tabelę Apartments tylko jeśli nie istnieje
            migrationBuilder.Sql(@"
                IF OBJECT_ID('Apartments', 'U') IS NULL
                BEGIN
                    CREATE TABLE Apartments (
                        ApartmentID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        Pietro INT NOT NULL,
                        LiczbaPieterWBudynku INT NOT NULL,
                        Metraz FLOAT NOT NULL,
                        LiczbaPokoi INT NOT NULL,
                        WcRazemZLazienka BIT NOT NULL,
                        Balkon BIT NOT NULL,
                        Winda BIT NOT NULL,
                        Piwnica BIT NOT NULL,
                        OgrzewaniePodlogowe BIT NOT NULL,
                        Klimatyzacja BIT NOT NULL,
                        Garaz BIT NOT NULL,
                        MiejsceParkingoweNaZewnatrz BIT NOT NULL,
                        Ogrod BIT NOT NULL,
                        Taras BIT NOT NULL
                    )
                END
            ");

            // 3. Utwórz indeks i FK tylko jeśli nie istnieją
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_MyFiles_Apartments_ApartmentID'
                )
                BEGIN
                    CREATE INDEX IX_MyFiles_ApartmentID ON MyFiles(ApartmentID);
                    
                    ALTER TABLE MyFiles
                    ADD CONSTRAINT FK_MyFiles_Apartments_ApartmentID
                    FOREIGN KEY (ApartmentID) REFERENCES Apartments(ApartmentID)
                    ON DELETE NO ACTION;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Usuń FK i indeks, jeśli istnieją
            migrationBuilder.Sql(@"
                IF OBJECT_ID('MyFiles', 'U') IS NOT NULL
                    AND EXISTS (
                        SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_MyFiles_Apartments_ApartmentID'
                    )
                BEGIN
                    ALTER TABLE MyFiles DROP CONSTRAINT FK_MyFiles_Apartments_ApartmentID;
                    DROP INDEX IX_MyFiles_ApartmentID ON MyFiles;
                END
            ");

            // 2. Usuń tabelę Apartments jeśli istnieje
            migrationBuilder.Sql(@"
                IF OBJECT_ID('Apartments', 'U') IS NOT NULL
                BEGIN
                    DROP TABLE Apartments;
                END
            ");

            // 3. Usuń kolumnę ApartmentID jeśli istnieje
            migrationBuilder.Sql(@"
                IF COL_LENGTH('MyFiles', 'ApartmentID') IS NOT NULL
                BEGIN
                    ALTER TABLE MyFiles DROP COLUMN ApartmentID;
                END
            ");
        }
    }
}

