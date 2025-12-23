using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> OnRegister([Bind("Username,Email,Password,Repass")] RegisterViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser newUser = await _userService.Register(user.Username, user.Password, user.Email);
                    return Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Register", user);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> OnLogin([Bind("Username,Password")] LoginViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userService.Login(user.Username, user.Password);
                    return Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Login", user);
        }
        public async Task<IActionResult> Logout() 
        {
            await _userService.Logout();
            return Redirect("/Users/Login");
        }
    }
}
