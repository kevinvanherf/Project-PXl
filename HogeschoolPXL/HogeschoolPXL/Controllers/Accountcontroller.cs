using HogeschoolPXL.Data;
using HogeschoolPXL.Data.DefaultData;
using HogeschoolPXL.Models;
using HogeschoolPXL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HogeschoolPXL.Controllers
{
    public class Accountcontroller : Controller
    {
        HogeschoolPXLDbContext _context;
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        
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
                identityUser.UserName = user.Email;
                
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
            //return View("Login");
            return RedirectToAction("Login");

        }
        #endregion
        #region Role Change
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> index()
        {
           
            var users = new IdentityViewModel();
            var vakLector = await _userManager.Users.ToListAsync();
            foreach (var role in vakLector)
            {

            }



            return View(vakLector);
        }
        [Authorize (Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> RoleChange(string? id)
        {
            var userrole = new IdentityViewModel();
            var user =  _context.Users.Where(x=> x.Id== id).SingleOrDefault();
            var userInRole =  _context.UserRoles.Where(x=> x.UserId== id).Select(x=> x.RoleId).ToList();
            userrole.Id = id;
            userrole.Username = user.UserName;
            userrole.Email= user.Email;
            userrole.RoleID = userInRole.ToString();
            return View(userrole);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleChange(string id, IdentityViewModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var  _user = new IdentityUser();
                    _user.Id = user.Id;
                    _user.UserName = user.Username ; 
                    _user.Email = user.Email;
                    var role = _roleManager.FindByIdAsync(user.RoleID);
                    _userManager.RemoveFromRoleAsync(_user , role.ToString());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_userManager.Users.Any(e => e.Id == user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(RoleChange));
            }
            return View(user);
        }
        #endregion

    }
}
