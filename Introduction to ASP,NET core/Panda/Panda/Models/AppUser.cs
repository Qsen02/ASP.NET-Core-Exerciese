using Microsoft.AspNetCore.Identity;

namespace Panda.Models
{
    public class AppUser: IdentityUser
    {
        public RoleType Role { get; set; }
    }
}
