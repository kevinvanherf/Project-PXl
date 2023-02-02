using HogeschoolPXL.Data;
using HogeschoolPXL.Data.DefaultData;
using HogeschoolPXL.Models;
using HogeschoolPXL.Models.ViewModels;
using HogeschoolPXL.Models.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Data;

namespace HogeschoolPXL.Controllers
{
    public class Accountcontroller : Controller
    {
        HogeschoolPXLDbContext _context;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        
        public Accountcontroller(HogeschoolPXLDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
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
                var identityUser = new User();
                identityUser.FirstName = user.FirstName;
                identityUser.LastName = user.LastName;
                identityUser.Email = user.Email;
                identityUser.UserName = user.Username;
                if (emailcontroleren(identityUser))
                {


                    var identityResult = await _userManager.CreateAsync(identityUser, user.Password); // create een accoutn 
                    if (identityResult.Succeeded)
                    {

                        var roleResult = await _userManager.AddToRoleAsync(identityUser, Roles.NwGebruiker); // geeft dat accoutn een roll
                        if (roleResult.Succeeded)
                            return View("Login");
                        else
                        {
                            ModelState.AddModelError("", "Problemen met toekennen van rol!");
                            return View(user);
                        }
                    }


                    foreach (var errors in identityResult.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "het email is al in gebruik je moet een andere nemen!");
                    return View(user);
                }
            }
                return View(user);
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
        public async Task<IActionResult> index(string Search)
        {
           var userss = new List<IdentityViewModel>();
            
                var vakLector = await _context.Users
                .ToListAsync();
            foreach (var role in vakLector)
            {
                var users = new IdentityViewModel();
                users.Id = role.Id;
                users.FirstName= role.FirstName;
                users.LastName= role.LastName;
                users.Username = role.UserName;
                users.Email = role.Email;
                users.RoleID = _context.UserRoles.Where(x=> x.UserId == role.Id).Select(x=> x.RoleId).FirstOrDefault();
                users.Roles = await _context.Roles.FirstOrDefaultAsync(x=> x.Id==users.RoleID);
                userss.Add(users);
            }



            return View(userss.Where(x => (x.FirstName + " " + x.LastName + " " + x.Username + " " + x.Email + " " + x.Roles.Name ).Contains((Search == null) ? "" : Search)));
        }
        [Authorize (Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> RoleChange(string? id)
        {
            var userrole = new IdentityViewModel();
            var user =  _context.Users.Where(x=> x.Id== id).SingleOrDefault();
            ViewData["IdentityUserId"] = _context.Roles.Select(x=> new SelectListItem {Text = x.Name , Value = x.Id  });
            userrole.Id = id;
            userrole.FirstName = user.FirstName;
            userrole.LastName = user.LastName;
            userrole.Username = user.UserName;
            userrole.Email= user.Email;
            userrole.RoleID = _context.UserRoles.Where(x => x.UserId == userrole.Id).Select(x => x.RoleId).FirstOrDefault();
            userrole.Roles = await _context.Roles.FirstOrDefaultAsync(x => x.Id == userrole.RoleID);
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
                    var role = await _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).FirstOrDefaultAsync();
                    var _user = new User();
                    _user =  _userManager.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                    
                    _user.FirstName = user.FirstName;
                    _user.LastName = user.LastName;
                    _user.UserName = user.Username ;
                    _user.NormalizedUserName = user.Username;
                    _user.Email = user.Email;
                    _user.NormalizedEmail= user.Email;
                    
                    //var role = _roleManager.FindByIdAsync(user.RoleID);
                    //_userManager.RemoveFromRoleAsync(_user , role);
                    
                    //await _context.SaveChangesAsync();

                    _context.UserRoles.Remove(new IdentityUserRole<string>
                    {
                        RoleId = role,
                        UserId = user.Id

                    });
                    _context.UserRoles.Add(new IdentityUserRole<string>
                    {
                        RoleId = user.RoleID,
                        UserId = user.Id

                    });
                    _context.SaveChanges();
                    _context.Update(_user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(index));
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
        #region controle methodes 

        public bool emailcontroleren(User user)
        {
            bool toegang = true;
            var accaunts = _context.Users.ToList();
            foreach (var accaount in accaunts)
            {
                if (accaount.Email == user.Email)
                {
                    toegang = false;
                    break;
                }
            }
            return toegang;
        }
        #endregion

    }
}
