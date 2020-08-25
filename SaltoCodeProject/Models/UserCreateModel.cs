using System.ComponentModel.DataAnnotations;

namespace SaltoCodeProject.Models
{
    public class UserCreateModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
