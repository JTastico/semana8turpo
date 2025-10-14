namespace Lab8_JamilTurpo.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IClientRepository Clients { get; }
    IProductRepository Products { get; }
    Task<int> CompleteAsync();
}