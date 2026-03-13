using AutoMapper;
using Contracts.Skus;
using Domain.SKUs;

namespace Application.AutoMapperProfiles;

public class SkuProfile : Profile
{
    public SkuProfile()
    {
        CreateMap<Sku, SkuDto>();
        CreateMap<SkuTranslation, SkuTranslationDto>()
            .ReverseMap();
        
        CreateMap<CreateUpdateSkuDto, Sku>()
            .ForMember(dest => dest.ProductId,
                opt => opt.Ignore());
    }
}