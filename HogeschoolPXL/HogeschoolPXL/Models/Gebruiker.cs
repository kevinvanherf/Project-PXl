using HogeschoolPXL.Models.ViewModels.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Gebruiker
    {
        public int GebruikerId { get; set; }
        
        public string? Naam { get; set; }
        public string? VoorNaam { get; set; }
		[EmailAddress]
		public string? Email { get; set; }

        //public string? UserIs { get; set; }

        //public User? User { get; set; }
    }
}
