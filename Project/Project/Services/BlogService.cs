using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Models;

namespace Project.Services
{
    public class BlogService
    {
        private readonly BlogContext _context;
        public BlogService(BlogContext context) 
        { 
            _context = context; 
        }

        public async Task<List<Blog>> GetAllBlogs() 
        {
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            return blogs;
        }
        public async Task<Blog> GetBlogById(int blogId) 
        {
            Blog blog = await _context.Blogs.Include(el=>el.Author).FirstOrDefaultAsync(el=>el.Id == blogId);
            return blog;
        }
        public async Task<Blog> CreateBlog(string title, string content, string userId) 
        {
            Blog newBlog = new Blog()
            {
                Title = title,
                Content = content,
                CreatedDate = DateTime.UtcNow,
                AuthorId = userId
            };
            _context.Blogs.Add(newBlog);
            await _context.SaveChangesAsync();
            return newBlog;
        }
        public async void DeleteBlog(int blogId) 
        {
            await _context.Blogs.Where(el=>el.Id == blogId).ExecuteDeleteAsync();
        }
        public async Task<Blog> EditBlog(int blogId, string title, string content) 
        {
            Blog blog = await GetBlogById(blogId);
            blog.Title = title;
            blog.Content = content;
            await _context.SaveChangesAsync();
            return blog;
        }
    }
}
