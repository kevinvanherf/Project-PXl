using HogeschoolPXL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HogeschoolPXL.Controllers
{
    public class Accountcontroller
    {
        HogeschoolPXLDbContext _context;
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        public Accountcontroller(HogeschoolPXLDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context= context;
            _userManager= userManager;
            _signInManager= signInManager;
        }
        #region Login 
        #endregion
        #region Register  
        #endregion
        #region Logout 
        //public async Task<IActionResult> LogoutAsync()
        //{
        //    await _signInManager.SignOutAsync(); 
        //    return View("Login"); 
        //}
        #endregion

    }
}
