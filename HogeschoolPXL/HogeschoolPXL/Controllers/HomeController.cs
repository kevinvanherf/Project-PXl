using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {

            return View(_context.Inschrijving.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}