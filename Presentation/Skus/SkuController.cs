using Contracts.Skus;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Skus;

[ApiController]
[Route("api/[controller]")]
public class SkuController(
    ISkuService skuService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var skus = await skuService.GetListAsync();
        
        return Ok(skus);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var sku = await skuService.GetByIdAsync(id);
            return Ok(sku);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUpdateSkuDto createUpdateSkuDto)
    {
        try
        {
            var sku = await skuService.CreateAsync(createUpdateSkuDto);
            return Ok(sku);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CreateUpdateSkuDto updateSkuDto)
    {
        try
        {
            var sku = await skuService.UpdateAsync(id, updateSkuDto);
            return Ok(sku);
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
            await skuService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
