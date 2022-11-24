using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogeschoolPXL.Migrations
{
    public partial class gebruiker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Gebruiker",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIs",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_UserId",
                table: "Gebruiker",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruiker_AspNetUsers_UserId",
                table: "Gebruiker",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruiker_AspNetUsers_UserId",
                table: "Gebruiker");

            migrationBuilder.DropIndex(
                name: "IX_Gebruiker_UserId",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "UserIs",
                table: "Gebruiker");
        }
    }
}
