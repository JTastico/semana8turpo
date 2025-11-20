using Lab8_JamilTurpo.Data;
using Lab8_JamilTurpo.Models;
using Lab8_JamilTurpo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8_JamilTurpo.Services;

public class ProductService : IProductService
{
    // Para consultas de solo lectura, es común inyectar el DbContext directamente.
    private readonly LINQExampleContext _context;

    public ProductService(LINQExampleContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsPricierThanAsync(decimal price)
    {

        var productsQuery = _context.Products;

        var filteredQuery = productsQuery.Where(p => p.Price > price);
        
        return await filteredQuery.ToListAsync();
    }
    
    public async Task<Product?> GetMostExpensiveProductAsync()
    {
        var mostExpensiveProduct = await _context.Products
            .OrderByDescending(p => p.Price)
            .FirstOrDefaultAsync();

        return mostExpensiveProduct;
    }
    
    public async Task<decimal> GetAverageProductPriceAsync()
    {
        var averagePrice = await _context.Products
            .AverageAsync(p => p.Price);

        return averagePrice;
    }
    
    public async Task<IEnumerable<Product>> GetProductsWithoutDescriptionAsync()
    {

        var productsWithoutDescription = await _context.Products
            .Where(p => string.IsNullOrEmpty(p.Description))
            .ToListAsync();

        return productsWithoutDescription;
    }
}