﻿using HogeschoolPXL.Models;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;
using System;

namespace HogeschoolPXL.Data.DefaultData
{
    public class SeeData
    {
        public static void Populate(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
               var _context = scope.ServiceProvider.GetRequiredService<HogeschoolPXLDbContext>();
                if (!_context.Gebruiker.Any())
                {
                   var kevin=  new Gebruiker {  Naam = "Vanherf", VoorNaam="Kevin", Email="Kevin.vanherf@icloud.com" };
                   var Kristof =  new Gebruiker { Naam = "Palmaers", VoorNaam="Kristof", Email= "Kristof.Palmaers@PXL.BE" };
                    _context.Gebruiker.AddRange(kevin, Kristof);
                    var Lector = new Lector { GebruikerId = 2 };
                    _context.Lector.Add(Lector);
                    var boek = new Handboek { Titel = "C# Web1", KostPrijs = 20.99M, Afbeelding = "cWeb.jpg", UitgifteDatum = new DateTime(2021, 09, 16) };
                    _context.Handboek.Add(boek);
                    var vak = new Vak { HandboekID = 1, StudiePunten = 6, VakNaam = "C# Web1" };
                    _context.Vak.Add(vak);
                    var vakLector = new VakLector { LectorId = 1, VakId = 1 };
                    _context.VakLector.Add(vakLector);
                    var student =  new Student { GebruikerId = 1 };
                    _context.Student.Add(student);
                    var vakLeerkact = new VakLector { LectorId = 1, VakId = 1 };
                    var academijar = new AcademieJaar { StartDatum = new DateTime(2021, 09, 20) };
                    _context.AcademieJaar.Add(academijar);
                    var inschrijving = new Inschrijving { AcademieJaarId = 1,  StudentId= 1, VakLectorId= 1 };
                    _context.Inschrijving.Add(inschrijving);
                    _context.SaveChanges();  
                }
            }
        }
    }
}
