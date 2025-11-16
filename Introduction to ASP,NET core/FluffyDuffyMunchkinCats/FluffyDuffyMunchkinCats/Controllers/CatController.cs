using FluffyDuffyMunchkinCats.Data;
using FluffyDuffyMunchkinCats.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluffyDuffyMunchkinCats.Controllers
{
    public class CatController : Controller
    {
        private readonly CatContext _context;
        public CatController(CatContext context) 
        {
            _context = context;
        }
        [Route("/cat/add")]
        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> OnAdd([Bind("Name, Age, Breed, ImageUrl")] Cat cat)
        {
            if (ModelState.IsValid) 
            {
                _context.Cats.Add(cat);
                await _context.SaveChangesAsync();
                return Redirect("/Home/Index");
            }
            return View(cat);
        }
        [Route("/cats/{catId:int}")]
        public async Task<IActionResult> CatDetails(int catId) 
        {
            var cat = await _context.Cats.FindAsync(catId);
            return View(cat);
        }
    }
}
