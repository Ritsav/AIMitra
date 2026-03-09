using AutoMapper;
using Contracts.Products;
using Domain.Products;

namespace Application.AutoMapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductTranslation, ProductTranslationDto>();
    }
}