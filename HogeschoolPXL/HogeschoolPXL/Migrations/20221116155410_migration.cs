using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogeschoolPXL.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Inschrijving_VakLector_VakLectorId",
                table: "Inschrijving",
                column: "VakLectorId",
                principalTable: "VakLector",
                principalColumn: "VakLectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
