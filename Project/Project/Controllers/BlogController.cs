using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;
        private readonly UserService _userService;
        public BlogController(BlogService blogService,UserService userService)
        {
            _blogService = blogService;
            _userService = userService;
        }
        [Authorize]
        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> OnCreate([Bind("Title,Content")] CreateViewModel newBlog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser user = await _userService.GetCurrentUser();
                    await _blogService.CreateBlog(newBlog.Title, newBlog.Content,user.Id);
                    return Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Create", newBlog);
        }
        public async Task<IActionResult> Details(int id) 
        {
            Blog blog = await _blogService.GetBlogById(id);
            AppUser user= await _userService.GetCurrentUser();
            var model = new DetailsViewModel()
            {
                User = user,
                Blog = blog
            };
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id) 
        {
            await _blogService.DeleteBlog(id);
            return Redirect("/Home/Index");
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id) 
        {
            Blog blog = await _blogService.GetBlogById(id);
            var model = new EditViewModel()
            {
                Id = id,
                Title = blog.Title,
                Content = blog.Content,
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OnEdit([Bind("Id,Title,Content")] EditViewModel updatedBlog) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _blogService.EditBlog(updatedBlog.Id, updatedBlog.Title, updatedBlog.Content);
                    return Redirect($"/Blog/Details/{updatedBlog.Id}");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Edit", updatedBlog);
        }
    }
}
