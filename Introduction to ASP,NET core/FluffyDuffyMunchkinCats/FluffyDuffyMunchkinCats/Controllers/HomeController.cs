using System.Diagnostics;
using FluffyDuffyMunchkinCats.Data;
using FluffyDuffyMunchkinCats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluffyDuffyMunchkinCats.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CatContext _context;

        public HomeController(ILogger<HomeController> logger, CatContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cats = await _context.Cats.ToListAsync();
            return View(cats);
        }

        public IActionResult RedirectToAdd() 
        {
            return Redirect("/Cat/Add");
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
