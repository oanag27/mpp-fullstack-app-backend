using System.ComponentModel.DataAnnotations;

namespace mmp_prj.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?:{}|<>]).{8,}$", ErrorMessage = "Password must include uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
