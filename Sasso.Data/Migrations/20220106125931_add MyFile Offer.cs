using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class addMyFileOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyFileFileID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectsID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    OfferID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextMain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaItem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.OfferID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyFiles_MyFileFileID",
                table: "MyFiles",
                column: "MyFileFileID");

            migrationBuilder.CreateIndex(
                name: "IX_MyFiles_OfferID",
                table: "MyFiles",
                column: "OfferID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_MyFiles_MyFileFileID",
                table: "MyFiles",
                column: "MyFileFileID",
                principalTable: "MyFiles",
                principalColumn: "FileID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_Offer_OfferID",
                table: "MyFiles",
                column: "OfferID",
                principalTable: "Offer",
                principalColumn: "OfferID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_MyFiles_MyFileFileID",
                table: "MyFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_Offer_OfferID",
                table: "MyFiles");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_MyFileFileID",
                table: "MyFiles");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_OfferID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "MyFileFileID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "OfferID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "ProjectsID",
                table: "MyFiles");
        }
    }
}
