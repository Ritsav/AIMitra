using Contracts.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Customers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(
    ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var customers = await customerService.GetListAsync();
        
        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var customer = await customerService.GetByIdAsync(id);
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUpdateCustomerDto createUpdateCustomerDto)
    {
        try
        {
            var customer = await customerService.CreateAsync(createUpdateCustomerDto);
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CreateUpdateCustomerDto updateCustomerDto)
    {
        try
        {
            var customer = await customerService.UpdateAsync(id, updateCustomerDto);
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await customerService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
