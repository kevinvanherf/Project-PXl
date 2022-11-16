﻿// <auto-generated />
using System;
using HogeschoolPXL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HogeschoolPXL.Migrations
{
    [DbContext(typeof(HogeschoolPXLDbContext))]
    partial class HogeschoolPXLDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HogeschoolPXL.Models.AcademieJaar", b =>
                {
                    b.Property<int>("AcademieJaarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AcademieJaarId"), 1L, 1);

                    b.Property<DateTime>("StartDatum")
                        .HasColumnType("datetime2");

                    b.HasKey("AcademieJaarId");

                    b.ToTable("AcademieJaar");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Gebruiker", b =>
                {
                    b.Property<int>("GebruikerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GebruikerId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VoorNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GebruikerId");

                    b.ToTable("Gebruiker");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Handboek", b =>
                {
                    b.Property<int>("HandboekID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HandboekID"), 1L, 1);

                    b.Property<string>("Afbeelding")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("KostPrijs")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UitgifteDatum")
                        .HasColumnType("datetime2");

                    b.HasKey("HandboekID");

                    b.ToTable("Handboek");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Inschrijving", b =>
                {
                    b.Property<int>("InschrijvingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InschrijvingId"), 1L, 1);

                    b.Property<int>("AcademieJaarId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("VakLectorId")
                        .HasColumnType("int");

                    b.HasKey("InschrijvingId");

                    b.HasIndex("AcademieJaarId");

                    b.HasIndex("StudentId");

                    b.HasIndex("VakLectorId");

                    b.ToTable("Inschrijving");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Lector", b =>
                {
                    b.Property<int>("LectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LectorId"), 1L, 1);

                    b.Property<int>("GebruikerId")
                        .HasColumnType("int");

                    b.HasKey("LectorId");

                    b.HasIndex("GebruikerId");

                    b.ToTable("Lector");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<int>("GebruikerId")
                        .HasColumnType("int");

                    b.HasKey("StudentId");

                    b.HasIndex("GebruikerId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Vak", b =>
                {
                    b.Property<int>("VakId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VakId"), 1L, 1);

                    b.Property<int?>("HandboekID")
                        .HasColumnType("int");

                    b.Property<int>("StudiePunten")
                        .HasColumnType("int");

                    b.Property<string>("VakNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VakId");

                    b.HasIndex("HandboekID");

                    b.ToTable("Vak");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.VakLector", b =>
                {
                    b.Property<int>("VakLectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VakLectorId"), 1L, 1);

                    b.Property<int>("LectorId")
                        .HasColumnType("int");

                    b.Property<int>("VakId")
                        .HasColumnType("int");

                    b.HasKey("VakLectorId");

                    b.HasIndex("LectorId");

                    b.HasIndex("VakId");

                    b.ToTable("VakLector");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Inschrijving", b =>
                {
                    b.HasOne("HogeschoolPXL.Models.AcademieJaar", "academieJaar")
                        .WithMany()
                        .HasForeignKey("AcademieJaarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HogeschoolPXL.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HogeschoolPXL.Models.VakLector", "vakLector")
                        .WithMany()
                        .HasForeignKey("VakLectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("academieJaar");

                    b.Navigation("vakLector");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Lector", b =>
                {
                    b.HasOne("HogeschoolPXL.Models.Gebruiker", "Gebruiker")
                        .WithMany()
                        .HasForeignKey("GebruikerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gebruiker");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Student", b =>
                {
                    b.HasOne("HogeschoolPXL.Models.Gebruiker", "Gebruiker")
                        .WithMany()
                        .HasForeignKey("GebruikerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gebruiker");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.Vak", b =>
                {
                    b.HasOne("HogeschoolPXL.Models.Handboek", "Handboek")
                        .WithMany()
                        .HasForeignKey("HandboekID");

                    b.Navigation("Handboek");
                });

            modelBuilder.Entity("HogeschoolPXL.Models.VakLector", b =>
                {
                    b.HasOne("HogeschoolPXL.Models.Lector", "Lector")
                        .WithMany()
                        .HasForeignKey("LectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HogeschoolPXL.Models.Vak", "vak")
                        .WithMany()
                        .HasForeignKey("VakId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lector");

                    b.Navigation("vak");
                });
#pragma warning restore 612, 618
        }
    }
}
