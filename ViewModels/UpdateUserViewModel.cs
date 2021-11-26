using System.ComponentModel.DataAnnotations;

namespace IAAPIUsers.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
