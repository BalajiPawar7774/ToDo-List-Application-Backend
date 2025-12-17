using Microsoft.EntityFrameworkCore;
using ToDoApplication.Dal;
using ToDoApplication.Model;

namespace ToDoApplication.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<User> IsValidUserAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<List<Todo>> GetListOfTodoes(int userId)
        {
            var todoes = await _context.Todos
                .Where(t => t.UserId == userId)
                .ToListAsync();
            return todoes;
        }

        public async Task<bool> UpdateStatus(int todoId, int status)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.TodoId == todoId);
            if (todo == null)
            {
                return false;
            }
            if(status == 3)
            {
                todo.IsCompleted = true;
            }
            todo.Status = (Enum.Status)status;
            _context.Todos.Update(todo);
            await  _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePriority(int todoId, int priority)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.TodoId == todoId);
            if (todo == null)
            {
                return false;
            }
            todo.Priority = (Enum.Priority)priority;
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<Todo>> GetNotCompletedOnly(int userId)
        {
            var todoes = await _context.Todos
                .Where(t => t.UserId == userId && t.IsCompleted == false)
                .ToListAsync();
            return todoes;
        }

      
        public async Task<List<Todo>> GetAccrdingtoPriority(int userId, int priority)
        {
            var todoes = await _context.Todos
                .Where(t => t.UserId == userId && t.Priority == (Enum.Priority)priority)
                .ToListAsync();
            return todoes;
        }

        public async Task<List<Todo>> GetAccrodingToStatus(int userId, int status)
        {
            var todoes = await _context.Todos
                .Where(t => t.UserId == userId && t.Status == (Enum.Status)status)
                .ToListAsync();
            return todoes;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


    }
}
