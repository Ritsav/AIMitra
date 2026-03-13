using AutoMapper;
using Contracts.Customers;
using Domain.Customers;

namespace Application.AutoMapperProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateUpdateCustomerDto, Customer>();
        CreateMap<Customer, CustomerDto>();
        
        CreateMap<CustomerTranslationDto, CustomerTranslation>()
            .ReverseMap();
    }
}