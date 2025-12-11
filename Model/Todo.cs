using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApplication.Enum;
namespace ToDoApplication.Model
{
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TodoId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; } = string.Empty;
        public Priority Priority { get; set; } = Priority.Low;
        public Status Status { get; set; } = Status.Created;

        [ForeignKey("User")]
        public int UserId { get; set; }

        public Todo()
        {
            this.CreatedAt = DateTime.Now;
        }

    }
}
