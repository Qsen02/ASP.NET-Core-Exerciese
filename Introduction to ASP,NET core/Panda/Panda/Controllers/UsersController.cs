using Microsoft.AspNetCore.Mvc;
using Panda.Data;
using Panda.Models;
using Panda.ViewModels;

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
            return View(new LoginViewModel());
        }
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> OnRegister([Bind("Username,Password,ConfirmPassword,Email")] RegisterViewModel user) 
        {
            if (ModelState.IsValid)
            {
                User newUser = new User { Username = user.Username, Password = user.Password, Email = user.Email };
                List<User> users=_context.Users.ToList();
                if (users.Count == 0)
                {
                    newUser.Role = (RoleType)1;
                }
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return Redirect("/Home/Index");
            }
            return View("Register",user);
        } 
    }
}
