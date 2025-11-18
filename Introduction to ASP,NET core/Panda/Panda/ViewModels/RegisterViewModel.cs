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
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage = "Password must match!")]
        public string ConfirmPassword { get; set; }
    }
}
