using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using Microsoft.AspNetCore.Authorization;
using HogeschoolPXL.Data.DefaultData;
using HogeschoolPXL.Models.ViewModels.Identity;

namespace HogeschoolPXL.Controllers
{
    [Authorize]
    public class GebruikersController : Controller
    {
        private readonly HogeschoolPXLDbContext _context;

        public GebruikersController(HogeschoolPXLDbContext context)
        {
            _context = context;
        }
        #region view actions
        [Authorize(Roles =Roles.Admin)]
        // GET: Gebruikers
        public async Task<IActionResult> Index(string Search)
        {
              return View(await _context.Gebruiker
                  .Include(x=> x.User)
                  .Where(x => (x.VoorNaam + " " + x.Naam).Contains((Search == null) ? "" : Search))
                  .ToListAsync());
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Gebruikers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gebruiker == null)
            {
                return NotFound();
            }

            var gebruiker = await _context.Gebruiker
                .Include(x=> x.User)
                .FirstOrDefaultAsync(m => m.GebruikerId == id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            return View(gebruiker);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Gebruikers/Create
        public IActionResult Create()
        {
            //ViewData["Users"] = _context.Users.Select(x => new SelectListItem {Text = $"{x.FirstName} {x.LastName}" , Value= x.Id });
            list();
            return View();
        }

        // POST: Gebruikers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GebruikerId,Naam,VoorNaam,Email ,UserId ")] Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                if (useridcontrole( gebruiker))
                {


                    _context.Add(gebruiker);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "de user kan je niet toe kennen deze is al gekopeld aan ieamand anders!");
                    return View(gebruiker);
                }
            }
            return View(gebruiker);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Gebruikers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gebruiker == null)
            {
                return NotFound();
            }
            list();
            //ViewData["Users"] = _context.Users.Select(x => new SelectListItem { Text = $"{x.FirstName} {x.LastName}", Value = x.Id });

            var gebruiker = await _context.Gebruiker.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }
            return View(gebruiker);
        }

        // POST: Gebruikers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Gebruiker gebruiker)
        {
           
            if (id != gebruiker.GebruikerId)
            {
             return NotFound();
            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Gebruiker.Update(gebruiker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GebruikerExists(gebruiker.GebruikerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }


            list();
            return View(gebruiker);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Gebruikers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gebruiker == null)
            {
                return NotFound();
            }

            var gebruiker = await _context.Gebruiker
                .FirstOrDefaultAsync(m => m.GebruikerId == id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            return View(gebruiker);
        }

        // POST: Gebruikers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gebruiker == null)
            {
                return Problem("Entity set 'HogeschoolPXLDbContext.Gebruiker'  is null.");
            }
            var gebruiker = await _context.Gebruiker.FindAsync(id);
            if (gebruiker != null)
            {
                _context.Gebruiker.Remove(gebruiker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool GebruikerExists(int id)
        {
          return _context.Gebruiker.Any(e => e.GebruikerId == id);
        }
        #endregion
        #region controle methodes
        public void list()
        {

            var lijst = _context.Users.Select(x => new SelectListItem { Text = $"{x.FirstName} {x.LastName}", Value = x.Id });
            
            ViewData["Users"] = lijst;
        }
        public bool useridcontrole(Gebruiker gebruiker)
        {
            bool controle = true;
            var gebruikers = _context.Gebruiker.ToList();
            foreach (var gb in gebruikers)
            {
                if (gb.GebruikerId == gebruiker.GebruikerId)
                {
                    if (gb.UserId == gebruiker.UserId)
                    {
                        break;
                    }
                }
                if (_context.Gebruiker.Any(e => e.UserId== gebruiker.UserId))
                {
                    controle= false;
                    break;
                }
            }
            return controle;
        }
        #endregion
    }
}
