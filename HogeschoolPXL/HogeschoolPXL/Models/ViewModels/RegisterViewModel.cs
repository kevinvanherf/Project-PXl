using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models.ViewModels
{
    public class RegisterViewModel : LoginViewModel 
    {
        [Display ( Name ="User Name")]
        public string? Username { get; set; }

    }
}
