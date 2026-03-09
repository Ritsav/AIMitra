namespace Domain.Products;

public interface IProductRepository
{
    public Task<Product> CreateAsync(Product product);
    public Task<Product> UpdateAsync(Product product);
    public Task DeleteAsync(Product product);
    
    public Task<Product?> GetByIdAsync(Guid id);
    public Task<List<Product>> GetListAsync();
}