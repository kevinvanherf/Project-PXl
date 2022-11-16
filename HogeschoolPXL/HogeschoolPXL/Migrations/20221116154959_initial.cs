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
                name: "Handboek",
                columns: table => new
                {
                    HandboekID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KostPrijs = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    UitgifteDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Afbeelding = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handboek", x => x.HandboekID);
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
                    table.ForeignKey(
                        name: "FK_Lector_Gebruiker_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruiker",
                        principalColumn: "GebruikerId",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Student_Gebruiker_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruiker",
                        principalColumn: "GebruikerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vak",
                columns: table => new
                {
                    VakId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VakNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudiePunten = table.Column<int>(type: "int", nullable: false),
                    HandboekID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vak", x => x.VakId);
                    table.ForeignKey(
                        name: "FK_Vak_Handboek_HandboekID",
                        column: x => x.HandboekID,
                        principalTable: "Handboek",
                        principalColumn: "HandboekID");
                });

            migrationBuilder.CreateTable(
                name: "VakLector",
                columns: table => new
                {
                    VakLectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LectorId = table.Column<int>(type: "int", nullable: false),
                    VakId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VakLector", x => x.VakLectorId);
                    table.ForeignKey(
                        name: "FK_VakLector_Lector_LectorId",
                        column: x => x.LectorId,
                        principalTable: "Lector",
                        principalColumn: "LectorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VakLector_Vak_VakId",
                        column: x => x.VakId,
                        principalTable: "Vak",
                        principalColumn: "VakId",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Inschrijving_AcademieJaar_AcademieJaarId",
                        column: x => x.AcademieJaarId,
                        principalTable: "AcademieJaar",
                        principalColumn: "AcademieJaarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inschrijving_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijving_AcademieJaarId",
                table: "Inschrijving",
                column: "AcademieJaarId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijving_StudentId",
                table: "Inschrijving",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijving_VakLectorId",
                table: "Inschrijving",
                column: "VakLectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lector_GebruikerId",
                table: "Lector",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_GebruikerId",
                table: "Student",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vak_HandboekID",
                table: "Vak",
                column: "HandboekID");

            migrationBuilder.CreateIndex(
                name: "IX_VakLector_LectorId",
                table: "VakLector",
                column: "LectorId");

            migrationBuilder.CreateIndex(
                name: "IX_VakLector_VakId",
                table: "VakLector",
                column: "VakId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inschrijving");

            migrationBuilder.DropTable(
                name: "AcademieJaar");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "VakLector");

            migrationBuilder.DropTable(
                name: "Lector");

            migrationBuilder.DropTable(
                name: "Vak");

            migrationBuilder.DropTable(
                name: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Handboek");
        }
    }
}
