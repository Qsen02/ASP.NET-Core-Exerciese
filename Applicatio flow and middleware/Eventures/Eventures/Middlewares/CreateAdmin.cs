using Eventures.Models;
using Microsoft.AspNetCore.Identity;

namespace Eventures.Middlewares
{
    public class CreateAdmin
    {
        private readonly RequestDelegate _next;
        public CreateAdmin(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager)
        {
            AppUser user = await userManager.FindByNameAsync("Qskata");
            if (user == null)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = "Qskata",
                    Email = "qsen@gmail.com",
                    FirstName = "Qsen",
                    LastName = "Genchev",
                    UCN = "8000",
                };
                await userManager.CreateAsync(appUser, "+Omega02");
                await userManager.AddToRoleAsync(appUser, "Admin");
            }

            await _next(context);
        }
    }
}
