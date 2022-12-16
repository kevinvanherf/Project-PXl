using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using HogeschoolPXL.Data.DefaultData;
using Microsoft.AspNetCore.Authorization;

namespace HogeschoolPXL.Controllers
{
    [Authorize]
    public class HandboeksController : Controller
    {
        private readonly HogeschoolPXLDbContext _context;

        public HandboeksController(HogeschoolPXLDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles  = $"{Roles.Admin} , {Roles.Lector}")]
        // GET: Handboeks
        public async Task<IActionResult> Index(string Search)
        {
            return View(await _context.Handboek.Where(x => x.Titel.Contains((Search == null)? "": Search))
            .ToListAsync());
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Handboeks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Handboek == null)
            {
                return NotFound();
            }

            var handboek = await _context.Handboek
                .FirstOrDefaultAsync(m => m.HandboekID == id);
            if (handboek == null)
            {
                return NotFound();
            }

            return View(handboek);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Handboeks/Create
        [HttpGet]
        public IActionResult Create()
        {
            var handboek = new NewHandboek();
            return View(handboek);
        }
        [Authorize(Roles = Roles.Admin)]
        // POST: Handboeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HandboekID,Titel,Price_string,UitgifteDatum,Afbeelding")] NewHandboek handboek)
        {
            if (ModelState.IsValid)
            {
                handboek.KostPrijs = decimal.Parse(handboek.Price_string);
                _context.Add(handboek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(handboek);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Handboeks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var newHandboek = new NewHandboek();
            if (id == null || _context.Handboek == null)
            {
                return NotFound();
            }

            var handboek =  await   _context.Handboek.FindAsync(id);
            if (handboek == null)
            { 
                return NotFound();
            }
            newHandboek.HandboekID = handboek.HandboekID;
            newHandboek.Titel = handboek.Titel;
            newHandboek.UitgifteDatum= handboek.UitgifteDatum;
            newHandboek.Afbeelding = handboek.Afbeelding;
            newHandboek.Price_string = handboek.KostPrijs.ToString();
            return View(newHandboek);
        }
        [Authorize(Roles = Roles.Admin)]
        // POST: Handboeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HandboekID,Titel,Price_string,UitgifteDatum,Afbeelding")] NewHandboek handboek)
        {
            if (id != handboek.HandboekID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    handboek.KostPrijs = decimal.Parse(handboek.Price_string);
                    _context.Update(handboek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HandboekExists(handboek.HandboekID))
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
            return View(handboek);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Handboeks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Handboek == null)
            {
                return NotFound();
            }

            var handboek = await _context.Handboek
                .FirstOrDefaultAsync(m => m.HandboekID == id);
            if (handboek == null)
            {
                return NotFound();
            }

            return View(handboek);
        }
        [Authorize(Roles = Roles.Admin)]
        // POST: Handboeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Handboek == null)
            {
                return Problem("Entity set 'HogeschoolPXLDbContext.Handboek'  is null.");
            }
            var handboek = await _context.Handboek.FindAsync(id);
            if (handboek != null)
            {
                _context.Handboek.Remove(handboek);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HandboekExists(int id)
        {
          return _context.Handboek.Any(e => e.HandboekID == id);
        }
    }
}
