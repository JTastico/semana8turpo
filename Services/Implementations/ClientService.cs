using System.Collections.Generic; // Asegúrate de tener este using
using System.Linq;

using Lab8_JamilTurpo.Data;
using Lab8_JamilTurpo.DTOs;
using Lab8_JamilTurpo.Models;
using Lab8_JamilTurpo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


public class ClientService : IClientService
{
    private readonly LINQExampleContext _context;

    public ClientService(LINQExampleContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Client>> GetClientsByNameAsync(string name)
    {
        var query = _context.Clients
            .Where(client => client.Name.StartsWith(name));

        return await query.ToListAsync();
    }
    
    public async Task<ClientOrderCountDto?> GetClientWithMostOrdersAsync()
    {

        var clientGroup = await _context.Orders
            .GroupBy(o => o.Clientid)
            .Select(g => new
            {
                ClientId = g.Key,
                OrderCount = g.Count()
            })
            .OrderByDescending(x => x.OrderCount)
            .FirstOrDefaultAsync();

        if (clientGroup == null)
        {
            return null;
        }

        var client = await _context.Clients
            .FindAsync(clientGroup.ClientId);

        if (client == null)
        {
            return null;
        }
        
        return new ClientOrderCountDto
        {
            ClientId = client.Clientid,
            ClientName = client.Name,
            ClientEmail = client.Email,
            OrderCount = clientGroup.OrderCount
        };
    }
    
    public async Task<IEnumerable<Product>> GetProductsPurchasedByClientAsync(int clientId)
    {
        var products = await _context.Orders
            .Where(o => o.Clientid == clientId) 
            .SelectMany(o => o.Orderdetails)
            .Select(od => od.Product)
            .Distinct()
            .ToListAsync();

        return products;
    }
    
    public async Task<IEnumerable<Client>> GetClientsByProductAsync(int productId)
    {
        // ⭐ ¡AQUÍ ESTÁ LA LÓGICA LINQ DEL EJERCICIO 12! ⭐
        var clients = await _context.Orderdetails
            .Where(od => od.Productid == productId)
            .Include(od => od.Order)
            .ThenInclude(o => o.Client)
            .Select(od => od.Order.Client)
            .Distinct()
            .ToListAsync();

        return clients;
    }
}