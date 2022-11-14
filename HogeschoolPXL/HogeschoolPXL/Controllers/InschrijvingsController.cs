using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using System.Collections.Immutable;

namespace HogeschoolPXL.Controllers
{
    public class InschrijvingsController : Controller
    {
        private readonly HogeschoolPXLDbContext _context;

        public InschrijvingsController(HogeschoolPXLDbContext context)
        {
            _context = context;
        }

        // GET: Inschrijvings
        public async Task<IActionResult> Index()
        {
              return View(await _context.Inschrijving.ToListAsync());
        }

        // GET: Inschrijvings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inschrijving == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijving
                .FirstOrDefaultAsync(m => m.InschrijvingId == id);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        // GET: Inschrijvings/Create
        public IActionResult Create()
        {
            
               
            var name = _context.VakLector.Where(x => x.VakId == x.vak.VakId).Select(x => x.vak.VakNaam);

            ViewBag.VakLector = new SelectList(_context.VakLector, "vakLectorId", "VakNaam");
            return View();
        }

        // POST: Inschrijvings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InschrijvingId,StudentId,VakLectorId,AcademieJaarId")] Inschrijving inschrijving)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inschrijving);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inschrijving);
        }

        // GET: Inschrijvings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inschrijving == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijving.FindAsync(id);
            if (inschrijving == null)
            {
                return NotFound();
            }
            return View(inschrijving);
        }

        // POST: Inschrijvings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InschrijvingId,StudentId,VakLectorId,AcademieJaarId")] Inschrijving inschrijving)
        {
            if (id != inschrijving.InschrijvingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inschrijving);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InschrijvingExists(inschrijving.InschrijvingId))
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
            return View(inschrijving);
        }

        // GET: Inschrijvings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inschrijving == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijving
                .FirstOrDefaultAsync(m => m.InschrijvingId == id);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        // POST: Inschrijvings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inschrijving == null)
            {
                return Problem("Entity set 'HogeschoolPXLDbContext.Inschrijving'  is null.");
            }
            var inschrijving = await _context.Inschrijving.FindAsync(id);
            if (inschrijving != null)
            {
                _context.Inschrijving.Remove(inschrijving);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InschrijvingExists(int id)
        {
          return _context.Inschrijving.Any(e => e.InschrijvingId == id);
        }
    }
}
