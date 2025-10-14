using Lab8_JamilTurpo.DTOs;
using Lab8_JamilTurpo.Models;

namespace Lab8_JamilTurpo.Services.Interfaces;

public interface IClientService
{
    Task<IEnumerable<Client>> GetClientsByNameAsync(string name);
    Task<ClientOrderCountDto?> GetClientWithMostOrdersAsync();
    Task<IEnumerable<Product>> GetProductsPurchasedByClientAsync(int clientId);
    Task<IEnumerable<Client>> GetClientsByProductAsync(int productId);

}