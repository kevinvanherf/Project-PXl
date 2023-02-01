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
using HogeschoolPXL.Data.DefaultData;
using Microsoft.AspNetCore.Authorization;
using System.Net.NetworkInformation;
using static System.Reflection.Metadata.BlobBuilder;

namespace HogeschoolPXL.Controllers
{
    [Authorize]
    public class InschrijvingsController : Controller
    {
        private readonly HogeschoolPXLDbContext _context;

        public InschrijvingsController(HogeschoolPXLDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Inschrijvings
        public async Task<IActionResult> Index(string Search , string Jaarid)
        {
            return View(await _context.Inschrijving
                .Include(x => x.academieJaar)
                .Include(x => x.vakLector).ThenInclude(v => v.vak)
                .Include(x => x.vakLector).ThenInclude(v => v.Lector).ThenInclude(l => l.Gebruiker)
                .Include(x => x.Student).ThenInclude(s => s.Gebruiker)
            .Where(x => (x.Student.Gebruiker.VoorNaam + " " + x.Student.Gebruiker.Naam + "" + x.vakLector.Lector.Gebruiker.VoorNaam + " " + x.vakLector.Lector.Gebruiker.Naam + " " + x.vakLector.vak.VakNaam + " " + x.academieJaar.StartDatum).Contains((Search == null) ? "" : Search)).Where(x=> Jaarid == null || x.academieJaar.AcademieJaarId == int.Parse( Jaarid))
				.ToListAsync()) ;
        }
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
        // GET: Inschrijvings/Create
        public IActionResult Create()
        {
            lijst(); // laat de viewdata in voor later te kunne gebruiken voor te selecteren 

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
                if (!NietDubbel(inschrijving)) 
                { 
                    _context.Add(inschrijving);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    ModelState.AddModelError("", "Je kunt geen leerling twee keeer voor de zelfde vak met het zelfde jaar zetten opnieuw . Sorry error ");
                    lijst();
                    return View(inschrijving);
                }
               
            }

            lijst();
            return View(inschrijving);
        }
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
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
        public void lijst()
        {
            var studentSelect = _context.Student.Where(x => x.GebruikerId == x.Gebruiker.GebruikerId)
               .Select(x => new SelectListItem { Text = $"{x.Gebruiker.VoorNaam} {x.Gebruiker.Naam}", Value = x.StudentId.ToString() });
            var VaklectorSelect = _context.VakLector.Where(_x => _x.VakId == _x.vak.VakId)
                .Select(_x => new SelectListItem { Text = $"{_x.vak.VakNaam} - {_x.Lector.Gebruiker.VoorNaam} {_x.Lector.Gebruiker.Naam}", Value = _x.VakLectorId.ToString() });
            ViewData["VakLector"] = VaklectorSelect;
            ViewData["Student"] = studentSelect;
            ViewBag.AcademiJaar = new SelectList(_context.AcademieJaar, "AcademieJaarId", "StartDatum");// select list makane voor de academie jaar te kunnen selecteren 
                                                                                                        //ViewBag.VakLector = new SelectList(_context.VakLector, "vakLectorId", "VakId"); 

        }

        private bool NietDubbel(Inschrijving inschrijving)
        {
            bool exists = false;
            var waardes  = _context.Inschrijving.Where(x=> x.StudentId ==inschrijving.StudentId);
            foreach (var student in waardes)
            {
                if (student.VakLectorId==inschrijving.VakLectorId && student.AcademieJaarId == inschrijving.AcademieJaarId)
                {
                    exists= true;
                }
            }
           

            return exists;
        }

    }

}
