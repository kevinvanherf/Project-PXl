using Microsoft.AspNetCore.Identity;

namespace HogeschoolPXL.Models.ViewModels
{
    public class IdentityViewModel
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? RoleID { get; set; }
        public IdentityUser? Users { get; set; }
        public IdentityRole? Roles { get; set; }
       
    }
}
