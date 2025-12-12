using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;
using Panda.Services;

namespace Panda
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<PandaContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<PandaContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<PackageService>();
            builder.Services.AddScoped<ReceiptsService>();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            CreateRoles(app);
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
