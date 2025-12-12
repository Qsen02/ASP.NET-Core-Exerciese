using Eventures.Services;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return Redirect("/Home/Index");
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> OnRegister([Bind("Username,Email,Password,Repass,FirstName,LastName,UCN")] RegisterViewModel user) 
        {
            if (ModelState.IsValid) 
            {
                await _userService.Register(user.Username, user.FirstName, user.LastName, user.Password, user.Email, user.UCN);
                return Redirect("/Home/Index");
            }
            return View("Register",user);
        }
        [HttpPost]
        public async Task<IActionResult> OnLogin([Bind("Username,Password")] LoginViewModel user) 
        {
            if (ModelState.IsValid)
            {
                await _userService.Login(user.Username,user.Password);
                return Redirect("/Home/Index");
            }
            return View("Login", user);
        }
    }
}
