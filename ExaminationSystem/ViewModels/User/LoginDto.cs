using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.ViewModels.User
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
