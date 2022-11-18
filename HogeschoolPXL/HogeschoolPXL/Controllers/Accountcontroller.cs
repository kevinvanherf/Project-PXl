using HogeschoolPXL.Data;
using HogeschoolPXL.Data.DefaultData;
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
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser();
                identityUser.Email = user.Email;
                identityUser.UserName = user.Username;
                var identityResult = await _userManager.CreateAsync(identityUser, user.Password);
                if (identityResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(identityUser, Roles.Student);
                    if (roleResult.Succeeded)
                        return View("Login");
                    else
                    {
                        ModelState.AddModelError("", "Problemen met toekennen van rol!");
                        return View();
                    }
                }
                    

                foreach (var errors in identityResult.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
            }
            return View();
        }
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
