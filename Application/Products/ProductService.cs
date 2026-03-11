using AutoMapper;
using Contracts.Languages;
using Contracts.Products;
using Contracts.Skus;
using Domain.Products;
using Domain.SKUs;
using Microsoft.Extensions.Logging;

namespace Application.Products;

public class ProductService(
    IProductRepository productRepository,
    ILanguageManager languageManager,
    IMapper mapper,
    ILogger<ProductService> logger) : IProductService
{
    public async Task<List<ProductDto>> GetListAsync()
    {
        var products = await productRepository.GetListAsync(); 
        return mapper.Map<List<Product>, List<ProductDto>>(products);
    }
    
    public async Task<ProductDto> CreateAsync(CreateProductDto input)
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
        
        product = await productRepository.CreateAsync(product);
        return mapper.Map<Product, ProductDto>(product);
    }

    private List<ProductTranslation> CreateTranslations(ICollection<ProductTranslationDto> translationDtos)
    {
        var translations = new List<ProductTranslation>();
        
        foreach (var translationDto in translationDtos)
        {
            var translation = new ProductTranslation()
            {
                Id = Guid.NewGuid(),
                LanguageId = translationDto.LanguageId,
                Name = translationDto.Name,
                Description = translationDto.Description
            };
            
            translations.Add(translation);
        }

        return translations;
    }

    private List<Sku> CreateSkus(ICollection<CreateSkuDto> skuDtos)
    {
        var skus = new List<Sku>();

        foreach (var skuDto in skuDtos)
        {
            var sku = new Sku()
            {
                Id = Guid.NewGuid(),
                Price = skuDto.Price,
                Quantity = skuDto.Quantity,
                Translations = CreateSkuTranslations(skuDto.Translations)
            };

            skus.Add(sku);
        }

        return skus;
    }

    private List<SkuTranslation> CreateSkuTranslations(ICollection<SkuTranslationDto> translationDtos)
    {
        var translations = new List<SkuTranslation>();

        foreach (var translationDto in translationDtos)
        {
            var translation = new SkuTranslation()
            {
                Id = Guid.NewGuid(),
                LanguageId = translationDto.LanguageId,
                Name = translationDto.Name,
                Description = translationDto.Description
            };

            translations.Add(translation);
        }

        return translations;
    }
}