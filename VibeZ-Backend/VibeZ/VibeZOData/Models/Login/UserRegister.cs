using System.ComponentModel.DataAnnotations;

namespace VibeZOData.Models.Login
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}