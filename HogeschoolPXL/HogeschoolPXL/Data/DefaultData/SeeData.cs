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
                    new Gebruiker { GebruikerId = 1, Naam = "Vanherf", VoorNaam="Kevin", Email="Kevin.vanherf@icloud.com" };
                }
                
                //context.database.ens
                if (!context.Student.Any())
                {
                    new Student { GebruikerId = 1 , StudentId= 12100954};
                }
            }
        }
    }
}
