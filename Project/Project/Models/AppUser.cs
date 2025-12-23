using Microsoft.AspNetCore.Identity;

namespace Project.Models
{
    public class AppUser : IdentityUser
    {
        public string Email { get; set; }
    }
}
