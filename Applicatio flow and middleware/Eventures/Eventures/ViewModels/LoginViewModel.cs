using System.ComponentModel.DataAnnotations;

namespace Eventures.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\-_.*~]{3,}$", ErrorMessage = "Username must be at least 3 symbols long!")]
        public string Username { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Password must be at least 5 symbols long!")]
        public string Password { get; set; }
    }
}
