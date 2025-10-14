// Archivo: Repositories/UnitOfWork.cs
using Lab8_JamilTurpo.Data;
using Lab8_JamilTurpo.Repositories.Interfaces;

namespace Lab8_JamilTurpo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LINQExampleContext _context;
    public IClientRepository Clients { get; private set; }
    public IProductRepository Products { get; private set; }

    public UnitOfWork(LINQExampleContext context)
    {
        _context = context;
        Clients = new ClientRepository(_context);
        Products = new ProductRepository(_context);

    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}