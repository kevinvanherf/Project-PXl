using HogeschoolPXL.Models.ViewModels.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Gebruiker
    {
        public int GebruikerId { get; set; }
        
        public string? Naam { get; set; }
        public string? VoorNaam { get; set; }
        //nog unique laten maken 
        [EmailAddress]
		public string? Email { get; set; }
        //nog unique laten maken 
        public string? UserId { get; set; }

        public User? User { get; set; }
    }
}
