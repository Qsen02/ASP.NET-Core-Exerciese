using Microsoft.AspNetCore.Mvc;
using Panda.Data;
using Panda.Models;

namespace Panda.Controllers
{
    public class UsersController : Controller
    {
        private readonly PandaContext _context;
        public UsersController(PandaContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OnRegister([Bind("Username,Password,Email")] User user, string ConfirmPassword) 
        {
            Console.WriteLine(ConfirmPassword);
            if (user.Password != ConfirmPassword) 
            {
                throw new Exception("Password must match!");
            }
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Redirect("/Home/Index");
            }
            return View();
        } 
    }
}
