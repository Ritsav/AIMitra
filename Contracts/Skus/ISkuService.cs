namespace Contracts.Skus;

public interface ISkuService
{
    public Task<SkuDto> CreateAsync(CreateUpdateSkuDto input);
    public Task<SkuDto> UpdateAsync(Guid id, CreateUpdateSkuDto input);
    public Task DeleteAsync(Guid id);
    
    public Task<SkuDto> GetByIdAsync(Guid id);
    public Task<List<SkuDto>> GetListAsync();
}