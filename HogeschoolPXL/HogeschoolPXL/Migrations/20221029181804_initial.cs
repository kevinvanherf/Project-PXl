using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogeschoolPXL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademieJaar",
                columns: table => new
                {
                    AcademieJaarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademieJaar", x => x.AcademieJaarId);
                });

            migrationBuilder.CreateTable(
                name: "Gebruiker",
                columns: table => new
                {
                    GebruikerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoorNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruiker", x => x.GebruikerId);
                });

            migrationBuilder.CreateTable(
                name: "Inschrijving",
                columns: table => new
                {
                    InschrijvingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    VakLectorId = table.Column<int>(type: "int", nullable: false),
                    AcademieJaarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inschrijving", x => x.InschrijvingId);
                });

            migrationBuilder.CreateTable(
                name: "Lector",
                columns: table => new
                {
                    LectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GebruikerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lector", x => x.LectorId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GebruikerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Vak",
                columns: table => new
                {
                    VakId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VakNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudiePunten = table.Column<int>(type: "int", nullable: false),
                    HandboekID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vak", x => x.VakId);
                });

            migrationBuilder.CreateTable(
                name: "VakLector",
                columns: table => new
                {
                    vakLectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LectorId = table.Column<int>(type: "int", nullable: false),
                    VakId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VakLector", x => x.vakLectorId);
                });

            migrationBuilder.CreateTable(
                name: "Handboek",
                columns: table => new
                {
                    HandboekID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KostPrijs = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    UitgifteDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Afbeelding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VakId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handboek", x => x.HandboekID);
                    table.ForeignKey(
                        name: "FK_Handboek_Vak_VakId",
                        column: x => x.VakId,
                        principalTable: "Vak",
                        principalColumn: "VakId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Handboek_VakId",
                table: "Handboek",
                column: "VakId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademieJaar");

            migrationBuilder.DropTable(
                name: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Handboek");

            migrationBuilder.DropTable(
                name: "Inschrijving");

            migrationBuilder.DropTable(
                name: "Lector");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "VakLector");

            migrationBuilder.DropTable(
                name: "Vak");
        }
    }
}
