namespace management.Domain.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<T> CreateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}