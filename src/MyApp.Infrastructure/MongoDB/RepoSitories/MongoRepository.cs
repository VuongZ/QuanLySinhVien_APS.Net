using MongoDB.Driver;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.MongoDB.RepoSitories;
public abstract class MonGoRepository <T> : IMongoRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;
    public MonGoRepository(MongoDbContext context,string collectionname)
    {
        _collection=context.GetCollection<T>(collectionname);
        
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public abstract Task<T?> GetByIdAsync(int id);
    public abstract Task UpdateAsync(int id, T document);
    public abstract Task DeleteAsync(int id);

    public async Task AddAsync(T document)
    {
        await _collection.InsertOneAsync(document);
    }

}