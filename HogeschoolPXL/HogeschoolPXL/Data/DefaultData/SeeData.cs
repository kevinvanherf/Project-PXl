using HogeschoolPXL.Models;

namespace HogeschoolPXL.Data.DefaultData
{
    public class SeeData
    {
        public static void Populate(WebApplication app)
        {
            using (var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<HogeschoolPXLDbContext>())
            {
                if (!context.Gebruiker.Any())
                {
                   var kevin=  new Gebruiker {  Naam = "Vanherf", VoorNaam="Kevin", Email="Kevin.vanherf@icloud.com" };
                   var Kristof =  new Gebruiker { Naam = "Palmaers", VoorNaam="Kristof", Email= "Kristof.Palmaers@PXL.BE" };
               context.Gebruiker.AddRange(kevin, Kristof);
                    var Lector = new Lector { GebruikerId = 2 };
                    context.Lector.Add(Lector);
                    var boek = new Handboek { Titel = "C# Web1", KostPrijs = 20.99M, Afbeelding = "cWeb.jpg", UitgifteDatum = new DateTime(2021, 09, 16) };
                    context.Handboek.Add(boek);
                    var vak = new Vak { HandboekID = 1, StudiePunten = 6, VakNaam = "C# Web1" };
                    context.Vak.Add(vak);
                    var vakLector = new VakLector { LectorId = 1, VakId = 1 };
                    context.VakLector.Add(vakLector);
                    var student =  new Student { GebruikerId = 1 };
                    context.Student.Add(student);
                    var vakLeerkact = new VakLector { LectorId = 20007064, VakId = 1 };
                    var academijar = new AcademieJaar { StartDatum = new DateTime(2021, 09, 20) };
                    context.AcademieJaar.Add(academijar);
                    var inschrijving = new Inschrijving { AcademieJaarId = 1,  StudentId= 12100954, VakLectorId= 1 };
                    context.Inschrijving.Add(inschrijving);
                    context.SaveChanges();  
                }
            }
        }
    }
}
