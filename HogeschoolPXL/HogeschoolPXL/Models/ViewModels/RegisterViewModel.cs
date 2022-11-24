using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models.ViewModels
{
    public class RegisterViewModel: LoginViewModel
    {
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Display ( Name ="User Name")]
        public string? Username { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password doesn't match!")]
		public string ConfirmPassword { get; set; }

	}
}
