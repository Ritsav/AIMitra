using AutoMapper;
using Shared.Custom.Bases;
using Shared.Custom.Interfaces;

namespace Application.Custom;

public class ReadOnlyService<TEntity, TEntityDto>(
    IRepository<TEntity> repository,
    IMapper mapper)
    : IReadOnlyService<TEntityDto>
    where TEntity : AuditedEntity
{
    public async Task<TEntityDto> GetByIdAsync(Guid id)
    {
        var entity = await repository.GetAsync(id);
        return mapper.Map<TEntity, TEntityDto>(entity);
    }

    public async Task<List<TEntityDto>> GetListAsync()
    {
        var entities = await repository.GetListAsync();
        return mapper.Map<List<TEntity>, List<TEntityDto>>(entities);
    }
}