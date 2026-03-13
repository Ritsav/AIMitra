using Application.Custom;
using AutoMapper;
using Contracts.Customers;
using Domain;
using Domain.Customers;
using Shared.Custom.Functions;
using Shared.Custom.Interfaces;
using CustomerTranslation = Domain.Customers.CustomerTranslation;

namespace Application.Customers;

public class CustomerService(
    IRepository<Customer> customerRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) 
        : ReadOnlyService<Customer, CustomerDto>(customerRepository, mapper), 
            ICustomerService
{
    private readonly IRepository<Customer> _customerRepository = customerRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
    {
        var customer = _mapper.Map<CreateUpdateCustomerDto, Customer>(input);
        
        await _customerRepository.CreateAsync(customer);
        await unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<Customer, CustomerDto>(customer);
    }

    public async Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input)
    {
        var customer = await _customerRepository.GetAsync(id);
        
        _mapper.Map(input, customer);

        CollectionMerger.MergeCollection(
            customer.Translations,
            input.Translations,
            (t) => t.LanguageId,
            (t) => t.LanguageId,
            CreateTranslations,
            (dto, entity) =>
            {
                entity.Name = dto.Name;
            });
        
        _customerRepository.Update(customer);
        await unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<Customer, CustomerDto>(customer);
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _customerRepository.GetAsync(id);
        
        _customerRepository.Delete(customer);
        await unitOfWork.SaveChangesAsync();
    }

    private List<CustomerTranslation> CreateTranslations(ICollection<CustomerTranslationDto> dtos)
    {
        return dtos.Select(x => new CustomerTranslation()
        {
            LanguageId = x.LanguageId,
            Name = x.Name
        }).ToList();
    }
}