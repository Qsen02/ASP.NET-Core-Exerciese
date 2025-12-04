using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Panda.Models;
using Panda.Services;

namespace Panda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;
        private readonly PackageService _packageService;

        public HomeController(ILogger<HomeController> logger, UserService userService,PackageService packageService)
        {
            _logger = logger;
            _userService = userService;
            _packageService = packageService;
        }
        public async Task<IActionResult> Index()
        {
            AppUser user = await _userService.GetCurrentUser();
            if (user != null)
            {
                List<Package> packages = await _packageService.GetAllPackagesForUser(user);
                List<Package> userPackages = packages.Where(el => el.Recipient.Username == user.UserName).ToList();
                ViewBag.Packages = packages;
            }
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
