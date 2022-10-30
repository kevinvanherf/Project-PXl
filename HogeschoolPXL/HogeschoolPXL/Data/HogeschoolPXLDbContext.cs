using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Models;

namespace HogeschoolPXL.Data
{
    public class HogeschoolPXLDbContext : DbContext
    {
        public HogeschoolPXLDbContext(DbContextOptions<HogeschoolPXLDbContext> options)
            : base(options)
        { }
        public DbSet<HogeschoolPXL.Models.Student> Student { get; set; }
        public DbSet<HogeschoolPXL.Models.Gebruiker> Gebruiker { get; set; }
        public DbSet<HogeschoolPXL.Models.Inschrijving> Inschrijving { get; set; }
        public DbSet<HogeschoolPXL.Models.Lector> Lector { get; set; }
        public DbSet<HogeschoolPXL.Models.Vak> Vak { get; set; }
        public DbSet<HogeschoolPXL.Models.VakLector> VakLector { get; set; }
        public DbSet<HogeschoolPXL.Models.AcademieJaar> AcademieJaar { get; set; }
        public DbSet<HogeschoolPXL.Models.Handboek> Handboek { get; set; }


    }
}
