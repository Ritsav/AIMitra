using AutoMapper;
using Contracts.Products;
using Domain.Products;
using Microsoft.Extensions.Logging;

namespace Application.Products;

public class ProductService(
    IProductRepository productRepository,
    IMapper mapper,
    ILogger<ProductService> logger) : IProductService
{
    public async Task<List<ProductDto>> GetListAsync()
    {
        var products = await productRepository.GetListAsync(); 
        return mapper.Map<List<Product>, List<ProductDto>>(products);
    }
    
    public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
    {
        logger.LogInformation("Creating new product");
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Translations = CreateTranslations(createProductDto.Translations)
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
}