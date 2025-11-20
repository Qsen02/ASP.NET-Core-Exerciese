using System.ComponentModel.DataAnnotations;

namespace Panda.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Email must be valid!")]
        public string Email { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
        ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage = "Password must match!")]
        public string ConfirmPassword { get; set; }
    }
}
