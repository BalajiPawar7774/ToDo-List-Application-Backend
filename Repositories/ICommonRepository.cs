namespace ToDoApplication.Repositories
{
    public interface ICommonRepository <T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}
