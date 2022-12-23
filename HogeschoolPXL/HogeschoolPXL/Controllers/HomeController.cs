using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using HogeschoolPXL.Views.Portfolio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HogeschoolPXL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HogeschoolPXLDbContext _context { get; set; }
        public HomeController(ILogger<HomeController> logger , HogeschoolPXLDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string search)
        {
            ViewData["search"]= search;
            return View(_context.Inschrijving.ToList());
        }
        public IActionResult Portfolio()
        {
            var model = new PortfolioModel();
           
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CursusPgInfo(int? id)
        {
            var inschrijving = await _context.Inschrijving
                .Where(x => x.StudentId == id)
                .Include(x => x.vakLector).ThenInclude(x => x.Lector).ThenInclude(x => x.Gebruiker)
                .Include(x => x.vakLector).ThenInclude(x => x.vak).ThenInclude(x => x.Handboek)
                .FirstOrDefaultAsync();
            return View(inschrijving);
        }
        public async Task<IActionResult> CursusPgInfoLector(int? id)
        {
            ViewData["Student"] = await _context.Inschrijving
                .Where(x => x.VakLectorId == id)
                .Include(x => x.Student).ThenInclude(x => x.Gebruiker)
                .Select(x => x.Student)
                .ToListAsync(); ;
            var inschrijving = await _context.Inschrijving
                .Where(x => x.VakLectorId == id)
                .Include(x => x.vakLector).ThenInclude(x => x.Lector).ThenInclude(x => x.Gebruiker)
                .Include(x => x.vakLector).ThenInclude(x => x.vak).ThenInclude(x => x.Handboek)
                .FirstOrDefaultAsync();
            return View(inschrijving);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}