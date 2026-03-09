using Domain.Products;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Products;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product)
    {
        product = (await dbContext.Products.AddAsync(product)).Entity;
        await dbContext.SaveChangesAsync();
        
        return product;
    }

    public Task<Product> UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetListAsync()
    {
        var products = await dbContext.Products.ToListAsync();

        return products;
    }
}