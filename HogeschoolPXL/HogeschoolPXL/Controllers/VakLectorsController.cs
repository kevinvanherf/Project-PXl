using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models;

namespace HogeschoolPXL.Controllers
{
    public class VakLectorsController : Controller
    {
        private readonly HogeschoolPXLDbContext _context;

        public VakLectorsController(HogeschoolPXLDbContext context)
        {
            _context = context;
        }

        // GET: VakLectors
        public async Task<IActionResult> Index()
        {
              return View(await _context.VakLector.ToListAsync());
        }

        // GET: VakLectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VakLector == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLector
                .FirstOrDefaultAsync(m => m.vakLectorId == id);
            if (vakLector == null)
            {
                return NotFound();
            }

            return View(vakLector);
        }

        // GET: VakLectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VakLectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("vakLectorId,LectorId,VakId")] VakLector vakLector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vakLector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vakLector);
        }

        // GET: VakLectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VakLector == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLector.FindAsync(id);
            if (vakLector == null)
            {
                return NotFound();
            }
            return View(vakLector);
        }

        // POST: VakLectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("vakLectorId,LectorId,VakId")] VakLector vakLector)
        {
            if (id != vakLector.vakLectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vakLector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakLectorExists(vakLector.vakLectorId))
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
            return View(vakLector);
        }

        // GET: VakLectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VakLector == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLector
                .FirstOrDefaultAsync(m => m.vakLectorId == id);
            if (vakLector == null)
            {
                return NotFound();
            }

            return View(vakLector);
        }

        // POST: VakLectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VakLector == null)
            {
                return Problem("Entity set 'HogeschoolPXLDbContext.VakLector'  is null.");
            }
            var vakLector = await _context.VakLector.FindAsync(id);
            if (vakLector != null)
            {
                _context.VakLector.Remove(vakLector);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakLectorExists(int id)
        {
          return _context.VakLector.Any(e => e.vakLectorId == id);
        }
    }
}
