using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Custom.Bases;
using Shared.Custom.Interfaces;

namespace Infrastructure.Repositories;

public class GenericRepository<T>(AppDbContext context) : IRepository<T>
    where T : AuditedEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T> GetAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        
        // TODO: Write Custom NotFoundException
        return entity ?? throw new KeyNotFoundException();
    }

    public async Task<List<T>> GetListAsync(
        int page = 1,
        int pageSize = 10)
    {
        return await _dbSet
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task CreateAsync(T entity)
    {
        entity.CreatedOn = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        entity.ModifiedOn = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
