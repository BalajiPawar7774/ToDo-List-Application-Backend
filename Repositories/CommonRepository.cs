
using Microsoft.EntityFrameworkCore;
using ToDoApplication.Dal;

namespace ToDoApplication.Repositories
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public CommonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if(entity == null)
            {
                return false;
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T?> UpdateAsync(int id, T entity)
        {
            var existingEntity = await GetByIdAsync(id);
            if(existingEntity == null)
            {
                return null;
            }
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
