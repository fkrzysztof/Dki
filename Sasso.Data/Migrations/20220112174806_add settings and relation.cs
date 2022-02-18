using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class addsettingsandrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageSettings");

            migrationBuilder.AddColumn<int>(
                name: "SettingsID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingsID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyFiles_SettingsID",
                table: "MyFiles",
                column: "SettingsID",
                unique: true,
                filter: "[SettingsID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_Settings_SettingsID",
                table: "MyFiles",
                column: "SettingsID",
                principalTable: "Settings",
                principalColumn: "SettingsID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_Settings_SettingsID",
                table: "MyFiles");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_SettingsID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "SettingsID",
                table: "MyFiles");

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
    }
}
