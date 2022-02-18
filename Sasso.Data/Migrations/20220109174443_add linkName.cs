using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class addlinkName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultings");

            migrationBuilder.DropTable(
                name: "Labs");

            migrationBuilder.AddColumn<string>(
                name: "LinkName",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkName",
                table: "Offers");

            migrationBuilder.CreateTable(
                name: "Consultings",
                columns: table => new
                {
                    ConsultingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextMain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultings", x => x.ConsultingID);
                });

            migrationBuilder.CreateTable(
                name: "Labs",
                columns: table => new
                {
                    LabID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextMain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labs", x => x.LabID);
                });
        }
    }
}
