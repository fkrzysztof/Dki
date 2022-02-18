using Microsoft.EntityFrameworkCore.Migrations;

namespace Sald.Data.Migrations
{
    public partial class myfilefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_MyFiles_MyFileFileID",
                table: "MyFiles");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_MyFileFileID",
                table: "MyFiles");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_OfferID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "MediaItem",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "MyFileFileID",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "ProjectsID",
                table: "MyFiles");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Name",
            //    table: "AspNetUserTokens",
            //    type: "nvarchar(128)",
            //    maxLength: 128,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "LoginProvider",
            //    table: "AspNetUserTokens",
            //    type: "nvarchar(128)",
            //    maxLength: 128,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProviderKey",
            //    table: "AspNetUserLogins",
            //    type: "nvarchar(128)",
            //    maxLength: 128,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "LoginProvider",
            //    table: "AspNetUserLogins",
            //    type: "nvarchar(128)",
            //    maxLength: 128,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_MyFiles_OfferID",
                table: "MyFiles",
                column: "OfferID",
                unique: true,
                filter: "[OfferID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MyFiles_OfferID",
                table: "MyFiles");

            migrationBuilder.AddColumn<string>(
                name: "MediaItem",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyFileFileID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectsID",
                table: "MyFiles",
                type: "int",
                nullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "Name",
            //    table: "AspNetUserTokens",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(128)",
            //    oldMaxLength: 128);

            //migrationBuilder.AlterColumn<string>(
            //    name: "LoginProvider",
            //    table: "AspNetUserTokens",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(128)",
            //    oldMaxLength: 128);

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProviderKey",
            //    table: "AspNetUserLogins",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(128)",
            //    oldMaxLength: 128);

            //migrationBuilder.AlterColumn<string>(
            //    name: "LoginProvider",
            //    table: "AspNetUserLogins",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(128)",
            //    oldMaxLength: 128);

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
        }
    }
}
