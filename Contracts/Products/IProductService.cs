namespace Contracts.Products;

public interface IProductService
{
    public Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
    // public Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto updateProductDto);
    // public Task DeleteAsync(Guid id);
    //
    // public Task<ProductDto> GetByIdAsync(Guid id);
    public Task<List<ProductDto>> GetListAsync();
}