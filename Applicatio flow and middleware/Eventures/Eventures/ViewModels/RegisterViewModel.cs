using System.ComponentModel.DataAnnotations;

namespace Eventures.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\-_.*~]{3,}$", ErrorMessage = "Username must be at least 3 symbols long!")]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email must be valid!")]
        public string Email { get; set; }
        [Required]
        [MinLength(5,ErrorMessage = "Password must be at least 5 symbols long!")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password must match!")]
        public string Repass { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$",ErrorMessage = "Unique Citizen Number must be exactly 10 numbers!")]
        public string UCN { get; set; }
    }
}
