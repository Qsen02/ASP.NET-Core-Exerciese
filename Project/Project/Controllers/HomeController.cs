using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService userService;
        private readonly BlogService blogService;

        public HomeController(ILogger<HomeController> logger,UserService userService, BlogService blogService)
        {
            _logger = logger;
            this.userService = userService;
            this.blogService = blogService;
        }

        public async  Task<IActionResult> Index()
        {
            List<Blog> blogs = await blogService.GetAllBlogs();

            return View(blogs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
