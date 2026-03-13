namespace Contracts.Customers;

public interface ICustomerService
{
    Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input);
    Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input);
    Task DeleteAsync(Guid id);
    
    Task<CustomerDto> GetByIdAsync(Guid id);
    Task<List<CustomerDto>> GetListAsync();
}