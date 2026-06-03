namespace MyApp.Domain.Repositories;

public interface IMongoRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T document);
    Task UpdateAsync(int id, T document);
    Task DeleteAsync(int id);
}