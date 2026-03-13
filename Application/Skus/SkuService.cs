using Application.Custom;
using AutoMapper;
using Contracts.Languages;
using Contracts.Skus;
using Domain;
using Domain.SKUs;
using Shared.Custom.Functions;
using Shared.Custom.Interfaces;

namespace Application.Skus;

public class SkuService(
    IMapper mapper,
    IRepository<Sku> skuRepository,
    IUnitOfWork unitOfWork,
    ILanguageManager languageManager) 
        : ReadOnlyService<Sku, SkuDto>(skuRepository, mapper),
            ISkuService
{
    private readonly IRepository<Sku> _skuRepository = skuRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<SkuDto> CreateAsync(CreateUpdateSkuDto input)
    {
        var sku = _mapper.Map<CreateUpdateSkuDto, Sku>(input);
        
        await _skuRepository.CreateAsync(sku);
        await unitOfWork.SaveChangesAsync();

        return _mapper.Map<Sku, SkuDto>(sku);
    }

    public async Task<SkuDto> UpdateAsync(Guid id, CreateUpdateSkuDto input)
    {
        // Collect all language IDs for validation
        var languageIds = input.Translations
            .Select(t => t.LanguageId)
            .ToList();
        
        await languageManager.ValidateLanguagesAsync(languageIds);

        var sku = await _skuRepository.GetAsync(id);
        
        // Handle translations update
        CollectionMerger.MergeCollection(
            sku.Translations,
            input.Translations,
            e => e.LanguageId,
            d => d.LanguageId,
            CreateTranslations,
            (dto, entity) =>
            {
                entity.Name = dto.Name;
                entity.Description = dto.Description;
            });
        
        _skuRepository.Update(sku);
        
        await unitOfWork.SaveChangesAsync();
        return _mapper.Map<Sku, SkuDto>(sku);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sku = await _skuRepository.GetAsync(id);
        
        _skuRepository.Delete(sku);
        await unitOfWork.SaveChangesAsync();
    }
    
    private List<SkuTranslation> CreateTranslations(ICollection<SkuTranslationDto> dtos) 
    {
        return dtos.Select(dto => new SkuTranslation()
        {
            Id = Guid.NewGuid(),
            LanguageId = dto.LanguageId,
            Name = dto.Name,
            Description = dto.Description
        }).ToList();
    }
}