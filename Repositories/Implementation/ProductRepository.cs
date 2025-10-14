using Lab8_JamilTurpo.Data;
using Lab8_JamilTurpo.Models;
using Lab8_JamilTurpo.Repositories.Interfaces;

namespace Lab8_JamilTurpo.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(LINQExampleContext context) : base(context)
    {
    }
}