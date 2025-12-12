using Eventures.Models;
using Microsoft.AspNetCore.Identity;

namespace Eventures.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<AppUser> _signInManager;
        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }
        public async Task<AppUser> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return await _userManager.GetUserAsync(user);
        }
        public async Task<AppUser> Register(string username,string firstName,string lastName, string password, string email,string UCN)
        {
            AppUser userEmail = await _userManager.FindByNameAsync(username);
            if (userEmail != null)
            {
                throw new Exception("User with this username already exist!");
            }
            AppUser userName =  await _userManager.FindByEmailAsync(email);
            if (userName != null)
            {
                throw new Exception("User with this email already exist!");
            }
            AppUser appUser = new AppUser()
            {
                UserName = username,
                Email = email,
                LastName = lastName,
                FirstName = firstName,
                UCN = UCN,
            };
            await _userManager.CreateAsync(appUser, password);
            await _userManager.AddToRoleAsync(appUser, "User");
            await _signInManager.SignInAsync(appUser, true);
            return appUser;
        }
        public async Task Login(string username, string password)
        {
            AppUser user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User or password don't match!");
            }
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
            {
                throw new Exception("User or password don't match!");
            }
            await _signInManager.SignInAsync(user, true);
        }
        public async Task Logout() 
        {
            await _signInManager.SignOutAsync();
        }
    }
}
