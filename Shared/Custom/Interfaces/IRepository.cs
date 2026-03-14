namespace Shared.Custom.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetAsync(Guid id);
    Task<List<T>> GetListAsync(
        int page = 1,
        int pageSize = 10);
    
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
