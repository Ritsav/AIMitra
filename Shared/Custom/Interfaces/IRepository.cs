namespace Shared.Custom.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetAsync(Guid id);
    Task<List<T>> GetListAsync();
    
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
