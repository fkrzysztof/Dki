using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class addcontextOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_Offer_OfferID",
                table: "MyFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "Offers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "OfferID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_Offers_OfferID",
                table: "MyFiles",
                column: "OfferID",
                principalTable: "Offers",
                principalColumn: "OfferID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_Offers_OfferID",
                table: "MyFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Offer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "OfferID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_Offer_OfferID",
                table: "MyFiles",
                column: "OfferID",
                principalTable: "Offer",
                principalColumn: "OfferID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
