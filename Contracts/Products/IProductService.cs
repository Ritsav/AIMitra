namespace Contracts.Products;

public interface IProductService
{
    public Task<ProductDto> CreateAsync(CreateUpdateProductDto input);
    public Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input);
    public Task DeleteAsync(Guid id);
    
    public Task<ProductDto> GetByIdAsync(Guid id);
    public Task<List<ProductDto>> GetListAsync();
}