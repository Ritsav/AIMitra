using Application.Custom;
using AutoMapper;
using Contracts.Languages;
using Contracts.Products;
using Domain;
using Domain.Products;
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
        
        await languageManager.ValidateLanguagesAsync(languageIds);
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Translations = CreateTranslations(input.Translations),
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
}