using System.ComponentModel.DataAnnotations;

namespace Panda.Models
{
    public enum RoleType { User,Admin}
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
    }
}
