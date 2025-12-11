using Microsoft.EntityFrameworkCore;
using ToDoApplication.Model;

namespace ToDoApplication.Dal
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
    }
}
