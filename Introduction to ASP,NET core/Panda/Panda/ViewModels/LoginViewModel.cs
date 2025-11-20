using System.ComponentModel.DataAnnotations;

namespace Panda.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
        ErrorMessage = "Password is required!")]
        public string Password { get; set; }
    }
}
