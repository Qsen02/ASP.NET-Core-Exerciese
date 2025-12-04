using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Models;
using Panda.Services;
using Panda.ViewModels;

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            List<User> users = await _userService.GetAllUsers();
            ViewBag.Users = users;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
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
        [Authorize]
        public async Task<IActionResult> Details(int id) {
            Package package = await _packagesService.GetPackageById(id);
            return View(package);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Pending() 
        {
            List<Package> packages = await _packagesService.GetPendingPackages();
            return View(packages);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnShip(int id) 
        {
            await _packagesService.ChangeToShipped(id);
            return RedirectToAction("Pending");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Shipped()
        {
            List<Package> packages = await _packagesService.GetShippedPackages();
            return View(packages);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnDeliver(int id)
        {
            await _packagesService.ChangeToDelivered(id);
            return RedirectToAction("Shipped");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delivered()
        {
            List<Package> packages = await _packagesService.GetDeliveredPackages();
            return View(packages);
        }
    }
}
