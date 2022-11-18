using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models.ViewModels
{
    public class RegisterViewModel :LoginViewModel
    {
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}
