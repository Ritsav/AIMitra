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

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateProductDto createProductDto)
    {
        var product = await productService.CreateAsync(createProductDto);
        
        return Ok(product);
    }
}