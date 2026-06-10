using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.Persistence.Repositories;
public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;
    public Repository (AppDbContext appContext)
    {
        _context=appContext;
        _dbSet=appContext.Set<T>();
    }
   public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
       return await _dbSet.FindAsync(id);
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if(entity == null)
        {
            return;
        }
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
