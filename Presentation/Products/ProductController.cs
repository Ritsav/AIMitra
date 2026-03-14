using Contracts.Products;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductController(
    IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var products = await productService.GetListAsync();
        
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var product = await productService.GetByIdAsync(id);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUpdateProductDto createUpdateProductDto)
    {
        try
        {
            var product = await productService.CreateAsync(createUpdateProductDto);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CreateUpdateProductDto updateProductDto)
    {
        try
        {
            var product = await productService.UpdateAsync(id, updateProductDto);
            return Ok(product);
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
            await productService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
