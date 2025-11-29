using Microsoft.AspNetCore.Mvc;
using Panda.Models;
using Panda.Services;
using Panda.ViewModels;
using System.Threading.Tasks;

namespace Panda.Controllers
{
    public class PackagesController : Controller
    {
        private readonly PackageService _packagesService;
        private readonly UserService _userService;
        public PackagesController(PackageService packagesService, UserService userService)
        {
            _packagesService = packagesService;
            _userService = userService;
        }
        public async Task<IActionResult> Create()
        {
            List<User> users = await _userService.GetAllUsers();
            ViewBag.Users = users;
            return View();
        }
        public async Task<IActionResult> OnCreate([Bind("Description,Weight,ShippingAddress,Recipient")] CreatePackageViewModel package) 
        {
            User user = await _userService.GetUserById(int.Parse(package.Recipient));
            if (ModelState.IsValid) 
            {
                await _packagesService.CreatePackage(package.Description, package.Weight, package.ShippingAddress, user);
                return Redirect("/Home/Index");
            }
            return View();
        }
    }
}
