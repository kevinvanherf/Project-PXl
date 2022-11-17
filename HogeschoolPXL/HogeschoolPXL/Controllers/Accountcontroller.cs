using HogeschoolPXL.Data;
using HogeschoolPXL.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HogeschoolPXL.Controllers
{
    public class Accountcontroller : Controller
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
        [HttpGet]
        public IActionResult Login()
        {         
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            var IdentityUser = await _userManager.FindByEmailAsync(login.Email);
            if (IdentityUser != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(IdentityUser.UserName, login.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Probleem met inloggen");
            return View();
        }
        #endregion
        #region Register  
        #endregion
        #region Logout 
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
        #endregion

    }
}
