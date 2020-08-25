using System.ComponentModel.DataAnnotations;

namespace SaltoCodeProject.Models
{
    public class LockCreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
