using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class addpageset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PageSettings",
                columns: table => new
                {
                    PageSettingsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageSettings", x => x.PageSettingsID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageSettings");
        }
    }
}
