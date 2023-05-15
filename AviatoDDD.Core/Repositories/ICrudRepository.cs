namespace AviatoDDD.Domain.Repositories;

public interface ICrudRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetOneAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<T?> DeleteAsync(T entity);
}