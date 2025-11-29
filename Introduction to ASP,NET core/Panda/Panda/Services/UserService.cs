using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;

namespace Panda.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PandaContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<AppUser> userManager, 
            PandaContext context, 
            SignInManager<AppUser> signInManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> Register(string username,string password,string email) 
        {
            var passwordHasher = new PasswordHasher<User>();
            bool isUsernameExists = await _context.Users.AnyAsync(el => el.Username == username);
            if (isUsernameExists)
            {
                throw new Exception("User with this username already exist!");
            }
            bool isEmailExists = await _context.Users.AnyAsync(el => el.Email == email);
            if (isUsernameExists)
            {
                throw new Exception("User with this email already exist!");
            }
            User user = new User() 
            { 
                Username = username,
                Email = email,
            };
            user.Password = passwordHasher.HashPassword(user, password);
            AppUser appUser = new AppUser()
            {
                UserName = username,
                Email = email,
            };
            await _userManager.CreateAsync(appUser,password);
            if (_context.Users.ToList().Count == 0)
            {
                user.Role = RoleType.Admin;
                appUser.Role = RoleType.Admin;
                await _userManager.AddToRoleAsync(appUser, "Admin");
            }
            else
            {
                user.Role = RoleType.User;
                appUser.Role = RoleType.User;
                await _userManager.AddToRoleAsync(appUser, "User");
            }
            await _signInManager.SignInAsync(appUser, true);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task Login(string username,string password) 
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(el => el.UserName == username);
            if (user == null)
            {
                throw new Exception("User or password don't match!");
            }
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed) 
            {
                throw new Exception("User or password don't match!");
            }
            await _signInManager.SignInAsync(user,true);
        }
        public async Task<AppUser> GetCurrentUser() 
        {
            var user = _httpContextAccessor.HttpContext.User;
            return await _userManager.GetUserAsync(user);
        }
        public async Task Logout() { 
            await _signInManager.SignOutAsync();
        }
        public async Task<List<User>> GetAllUsers() 
        { 
            List<User> users = _context.Users.ToList();
            return users;
        }

        public async Task<User> GetUserById(string id) 
        { 
            User user=await _context.Users.FindAsync(id);
            return user;
        }
    }
}
