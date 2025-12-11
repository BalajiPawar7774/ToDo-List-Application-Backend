using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ToDoApplication.Dto
{
    public class UserDto
    {
        [Required (ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required (ErrorMessage = "Email is required")]
        [EmailAddress (ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;
        [Required (ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
