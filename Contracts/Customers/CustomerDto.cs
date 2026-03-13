using Shared.Custom.Interfaces;

namespace Contracts.Customers;

public class CustomerDto : BaseCustomerDto, IGuidEntityDto
{
    public Guid Id { get; set; }
}