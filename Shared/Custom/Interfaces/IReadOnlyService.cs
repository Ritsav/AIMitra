namespace Shared.Custom.Interfaces;

public interface IReadOnlyService<TEntityDto>
{
    Task<TEntityDto> GetByIdAsync(Guid id);
    Task<List<TEntityDto>> GetListAsync();
}
