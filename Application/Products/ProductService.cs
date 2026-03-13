using Application.Custom;
using AutoMapper;
using Contracts.Languages;
using Contracts.Products;
using Contracts.Skus;
using Domain;
using Domain.Products;
using Domain.SKUs;
using Microsoft.Extensions.Logging;
using Shared.Custom.Functions;
using Shared.Custom.Interfaces;

namespace Application.Products;

public class ProductService(
    IRepository<Product> productRepository,
    ILanguageManager languageManager,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<ProductService> logger) 
        : ReadOnlyService<Product, ProductDto>(productRepository, mapper), 
            IProductService
{
    private readonly IMapper _mapper = mapper;
    private readonly IRepository<Product> _productRepository = productRepository;

    public async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
    {
        logger.LogInformation("Creating new product");
        
        var languageIds = input.Translations
            .Select(t => t.LanguageId)
            .ToList();
        
        languageIds.AddRange(input.Skus
            .SelectMany(x => x.Translations.Select(translation => translation.LanguageId).ToList())
            .Distinct()
            .ToList());
        
        await languageManager.ValidateLanguagesAsync(languageIds);
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Translations = CreateTranslations(input.Translations),
            Skus = CreateSkus(input.Skus)
        };
        
        await _productRepository.CreateAsync(product);
        await unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
    {
        logger.LogInformation("Updating product with id {ProductId}", id);
        
        var product = await _productRepository.GetAsync(id);
        
        // Collect all language IDs for validation
        var languageIds = input.Translations
            .Select(t => t.LanguageId)
            .ToList();
        
        languageIds.AddRange(input.Skus
            .SelectMany(x => x.Translations.Select(t => t.LanguageId))
            .Distinct());
        
        await languageManager.ValidateLanguagesAsync(languageIds);
        
        CollectionMerger.MergeCollection(
            product.Translations,
            input.Translations,
            e => e.LanguageId,
            d => d.LanguageId,
            CreateTranslations,
            (dto, entity) =>
            {
                entity.Name = dto.Name;
                entity.Description = dto.Description;
            });
        
        CollectionMerger.MergeCollection(
            product.Skus,
            input.Skus,
            e => e.Id,
            d => d.Id,
            CreateSkus,
            (dto, entity) =>
            {
                entity.Price = dto.Price;
                entity.Quantity = dto.Quantity;
                
                CollectionMerger.MergeCollection(
                    entity.Translations,
                    dto.Translations,
                    e => e.LanguageId,
                    d => d.LanguageId,
                    CreateSkuTranslations,
                    (tDto, tEntity) =>
                    {
                        tEntity.Name = tDto.Name;
                        tEntity.Description = tDto.Description;
                    });
            });
        
        _productRepository.Update(product);
        
        await unitOfWork.SaveChangesAsync();
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        logger.LogInformation("Deleting product with id {ProductId}", id);
        
        var product = await _productRepository.GetAsync(id);
        
        _productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
    }

    private List<ProductTranslation> CreateTranslations(ICollection<ProductTranslationDto> dtos) 
    {
        return dtos.Select(dto => new ProductTranslation()
        {
            Id = Guid.NewGuid(),
            LanguageId = dto.LanguageId,
            Name = dto.Name,
            Description = dto.Description
        }).ToList();
    }

    private List<Sku> CreateSkus(ICollection<CreateUpdateSkuDto> skuDtos)
    {
        return skuDtos.Select(skuDto => new Sku()
        {
            Id = Guid.NewGuid(),
            Price = skuDto.Price,
            Quantity = skuDto.Quantity,
            Translations = CreateSkuTranslations(skuDto.Translations)
        }).ToList();
    }

    private static List<SkuTranslation> CreateSkuTranslations(ICollection<SkuTranslationDto> translationDtos)
    {
        return translationDtos.Select(translationDto => new SkuTranslation()
        {
            Id = Guid.NewGuid(),
            LanguageId = translationDto.LanguageId,
            Name = translationDto.Name,
            Description = translationDto.Description
        }).ToList();
    }
}