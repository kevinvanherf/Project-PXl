using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using Microsoft.AspNetCore.Authorization;
using HogeschoolPXL.Data.DefaultData;

namespace HogeschoolPXL.Controllers
{
    [Authorize]
    public class VaksController : Controller
    {
        private  HogeschoolPXLDbContext _context;

        public VaksController(HogeschoolPXLDbContext context)
        {
            _context = context;
            
        }
        [Authorize(Roles =$"{Roles.Admin} , {Roles.Student}, {Roles.Lector}")]
        // GET: Vaks
        public async Task<IActionResult> Index(string Search)
        {
              return View(await _context.Vak
                  .Include(x=> x.Handboek)
                  .Where(x => (x.VakNaam + " " + x.StudiePunten + " " + x.Handboek.Titel).Contains((Search == null) ? "" : Search))
                  .ToListAsync());
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Vaks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vak == null)
            {
                return NotFound();
            }

            var vak = await _context.Vak
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Vaks/Create
        public IActionResult Create()
        {
            ViewBag.Boek = new SelectList(_context.Handboek, "HandboekID", "Titel");

            return View();
        }
        [Authorize(Roles = Roles.Admin)]
        // POST: Vaks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VakId,VakNaam,StudiePunten,HandboekID")] Vak vak)
        {
            if (ModelState.IsValid)
            {
                if(!BoekInGebruiker(vak.HandboekID)&& !VakNaamIngebruik(vak.VakNaam)){
                    _context.Add(vak);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "het boek is al in gerbuik of de naam is al in gebruik!!");
                    ViewBag.Boek = new SelectList(_context.Handboek, "HandboekID", "Titel");
                    return View(vak);
                }
            }
            ViewBag.Boek = new SelectList(_context.Handboek, "HandboekID", "Titel");
            return View(vak);
        }
       
        [Authorize(Roles = Roles.Admin)]
        // GET: Vaks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vak == null)
            {
                return NotFound();
            }

            var vak = await _context.Vak.FindAsync(id);
            if (vak == null)
            {
                return NotFound();
            }
            ViewBag.Boek = new SelectList(_context.Handboek, "HandboekID", "Titel");
            return View(vak);
        }
        [Authorize(Roles = Roles.Admin)]
        // POST: Vaks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VakId,VakNaam,StudiePunten,HandboekID")] Vak vak)
        {
            if (id != vak.VakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakExists(vak.VakId))
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
            ViewBag.Boek = new SelectList(_context.Handboek, "HandboekID", "Titel");
            return View(vak);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Vaks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vak == null)
            {
                return NotFound();
            }

            var vak = await _context.Vak
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }
        [Authorize(Roles = Roles.Admin)]
        // POST: Vaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vak == null)
            {
                return Problem("Entity set 'HogeschoolPXLDbContext.Vak'  is null.");
            }
            var vak = await _context.Vak.FindAsync(id);
            if (vak != null)
            {
                _context.Vak.Remove(vak);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakExists(int id)
        {
          return _context.Vak.Any(e => e.VakId == id);
        }
        #region creta controle
        // controle voor te kijken dat het boek niet al gebruikt word bij het vak 
        public bool BoekInGebruiker(int? id)
        {
            var controle = false;
            if (_context.Vak.Any(x => x.HandboekID == id))
            {
                controle = true;
            }

            return controle;

        }
        // controle als de vaknaam niet in gebruik is 
        public bool VakNaamIngebruik(string naam)
        {
            var controle = false;
            if (_context.Vak.Any(x => x.VakNaam.ToLower() == naam.ToLower()))
            {
                controle = true;
            }

            return controle;

        }
        #endregion
    }
}
