using HogeschoolPXL.Models;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;
using System;
using Microsoft.AspNetCore.Identity;
using System.Data;
using HogeschoolPXL.Models.ViewModels.Identity;

namespace HogeschoolPXL.Data.DefaultData
{
    public class SeeData
    {
        static HogeschoolPXLDbContext? _context;
        static RoleManager<IdentityRole>? _roleManager;
        static UserManager<User>? _userManager;
        public static async Task PopulateAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                
                _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                 _context = scope.ServiceProvider.GetRequiredService<HogeschoolPXLDbContext>();
                if (!_context.Gebruiker.Any())
                {
                    var kevin = new Gebruiker { Naam = "Vanherf", VoorNaam = "Kevin", Email = "Kevin.vanherf@icloud.com" };
                    var Kristof = new Gebruiker { Naam = "Palmaers", VoorNaam = "Kristof", Email = "Kristof.Palmaers@PXL.BE" };
                    _context.Gebruiker.AddRange(kevin, Kristof);
                    _context.SaveChanges();
                }
                if (!_context.Student.Any())
                {
                    var student = new Student { GebruikerId = 1 };
                    _context.Student.Add(student);
                    _context.SaveChanges();
                    var Lector = new Lector { GebruikerId = 2 };
                    _context.Lector.Add(Lector);
                    _context.SaveChanges();
                }
                if (!_context.Handboek.Any())
                {
                    var boek = new Handboek { Titel = "C# Web1", KostPrijs = 20.99M, Afbeelding = "cWeb.jpg", UitgifteDatum = new DateTime(2021, 09, 16) };
                    _context.Handboek.Add(boek);
                    _context.SaveChanges();
                }
                if (!_context.Vak.Any())
                {
                    var vak = new Vak { HandboekID = 1, StudiePunten = 6, VakNaam = "C# Web1" };
                    _context.Vak.Add(vak);
                    _context.SaveChanges();
                    var vakLector = new VakLector { LectorId = 1, VakId = 1 };
                    _context.VakLector.Add(vakLector);
                    _context.SaveChanges();
                    var academijar = new AcademieJaar { StartDatum = new DateTime(2021, 09, 20) };
                    _context.AcademieJaar.Add(academijar);
                    _context.SaveChanges();
                    var inschrijving = new Inschrijving { AcademieJaarId = 1,  StudentId= 1, VakLectorId = 1 };
                    _context.Inschrijving.Add(inschrijving);
                    _context.SaveChanges();  
                }
                await VoegRollenToeAsync();
				await CreateIdentityRecordAsync("kevin","Vanherf",Roles.Student, "student@pxl.be", "Student123!", Roles.Student);
				await CreateIdentityRecordAsync("Admin", "",Roles.Admin, "admin@pxl.be", "Admin456!", Roles.Admin);

			}
		}
        private static async Task VoegRollenToeAsync()
        {
            if (_roleManager != null && !_roleManager.Roles.Any())
            {
                await VoegRolToeAsync(Roles.NwGebruiker);
                await VoegRolToeAsync(Roles.Student);
                await VoegRolToeAsync(Roles.Lector);
                await VoegRolToeAsync(Roles.Admin);
            }
        }
        private static async Task VoegRolToeAsync(string roleName)
        {
            if (_roleManager != null && !await _roleManager.RoleExistsAsync(roleName))
            {
                IdentityRole role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
        }
		private static async Task CreateIdentityRecordAsync(string firstName, string lastName, string userName, string email, string pwd, string role)
		{

			if (_userManager != null && await _userManager.FindByEmailAsync(email) == null &&
					await _userManager.FindByNameAsync(userName) == null)
			{
				var identityUser = new User() {FirstName= firstName, LastName= lastName, Email = email, UserName = userName };
				var result = await _userManager.CreateAsync(identityUser, pwd);
				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(identityUser, role);
				}
			}
		}
	}
}
