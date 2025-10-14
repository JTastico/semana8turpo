using Lab8_JamilTurpo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab8_JamilTurpo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet("price-greater-than/{price}")]
    public async Task<IActionResult> GetProductsPricierThan(decimal price)
    {
        var products = await _productService.GetProductsPricierThanAsync(price);

        if (products == null || !products.Any())
        {
            return NotFound($"No se encontraron productos con un precio mayor a {price}.");
        }

        return Ok(products);
    }
    
    [HttpGet("most-expensive")]
    public async Task<IActionResult> GetMostExpensiveProduct()
    {
        var product = await _productService.GetMostExpensiveProductAsync();

        if (product == null)
        {
            return NotFound("No se encontraron productos en la base de datos.");
        }

        return Ok(product);
    }
    
    [HttpGet("average-price")]
    public async Task<IActionResult> GetAverageProductPrice()
    {
        var average = await _productService.GetAverageProductPriceAsync();
        
        return Ok(new { averageProductPrice = average });
    }
    
    [HttpGet("without-description")]
    public async Task<IActionResult> GetProductsWithoutDescription()
    {
        var products = await _productService.GetProductsWithoutDescriptionAsync();

        if (products == null || !products.Any())
        {
            return NotFound("No products were found without a description.");
        }

        return Ok(products);
    }
}
