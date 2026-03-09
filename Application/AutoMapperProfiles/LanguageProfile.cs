using AutoMapper;
using Contracts.Languages;
using Domain.Languages;

namespace Application.AutoMapperProfiles;

public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<Language, LanguageDto>();
    }
}