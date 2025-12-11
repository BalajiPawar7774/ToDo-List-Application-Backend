using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApplication.Enum;
namespace ToDoApplication.Dto
{
    public class TodoDto
    {
        [Required (ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;
        [Required (ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
        [Required (ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        [Required (ErrorMessage = "Priority is required")]
        public Priority Priority { get; set; }
    }
}
