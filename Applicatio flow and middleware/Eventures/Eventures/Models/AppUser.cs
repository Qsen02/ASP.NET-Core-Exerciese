using Microsoft.AspNetCore.Identity;

namespace Eventures.Models
{
    public enum RoleType { User,Admin}
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UCN { get; set; }
        public RoleType Role { get; set; }
    }
}
