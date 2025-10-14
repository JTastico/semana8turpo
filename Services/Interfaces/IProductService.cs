using Lab8_JamilTurpo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab8_JamilTurpo.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsPricierThanAsync(decimal price);
    Task<Product?> GetMostExpensiveProductAsync();
    Task<decimal> GetAverageProductPriceAsync();
    Task<IEnumerable<Product>> GetProductsWithoutDescriptionAsync();
}