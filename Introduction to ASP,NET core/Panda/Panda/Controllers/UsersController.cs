using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Models;
using Panda.Services;
using Panda.ViewModels;
using System.Linq.Expressions;

namespace Panda.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return Redirect("/Home/Index");
        }
        [HttpPost]
        public async Task<IActionResult> OnRegister([Bind("Username,Password,ConfirmPassword,Email")] RegisterViewModel user) 
        {
            try {
                if (ModelState.IsValid)
                {
                    User newUser = await _userService.Register(user.Username, user.Password, user.Email);
                    return Redirect("/Home/Index");
                }
            } catch (Exception ex) 
            {
                TempData["msg"]=ex.Message;
            }
            return View("Register", user);
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
    }
}
