using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class AddPageContents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //zmieniono w bazie danych

            //migrationBuilder.RenameColumn(
            //    name: "Ogród",
            //    table: "Apartments",
            //    newName: "Ogrod");

            migrationBuilder.CreateTable(
                name: "PageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Culture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageContents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageContents");

            //migrationBuilder.RenameColumn(
            //    name: "Ogrod",
            //    table: "Apartments",
            //    newName: "Ogród");
        }
    }
}
