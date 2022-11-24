using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogeschoolPXL.Migrations
{
    public partial class inschrijving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inschrijving_VakLectorId",
                table: "Inschrijving",
                column: "VakLectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inschrijving_VakLector_VakLectorId",
                table: "Inschrijving",
                column: "VakLectorId",
                principalTable: "VakLector",
                principalColumn: "VakLectorId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inschrijving_VakLector_VakLectorId",
                table: "Inschrijving");

            migrationBuilder.DropIndex(
                name: "IX_Inschrijving_VakLectorId",
                table: "Inschrijving");
        }
    }
}
