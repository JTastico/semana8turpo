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
    
    public async Task<IEnumerable<string>> GetClientNamesByProductAsync(int productId)
    {
        var clientNames = await _context.Orderdetails
            .Where(od => od.Productid == productId)
            .Select(od => od.Order.Client.Name)
            .Distinct()
            .ToListAsync();

        return clientNames;
    }
    
    public async Task<IEnumerable<ClientOrderDto>> GetAllClientsWithOrdersAsync()
    {
        var clientOrders = await _context.Clients
            .AsNoTracking()
            .Select(client => new ClientOrderDto
            {
                ClientName = client.Name,
                Orders = client.Orders
                    .Select(order => new OrderDto
                    {
                        OrderId = order.Orderid,
                        OrderDate = order.Orderdate
                    }).ToList()
            }).ToListAsync();

        return clientOrders;
    }
    
    public async Task<IEnumerable<ClientProductCountDto>> GetClientsWithProductCountAsync()
    {
        var clientsWithCount = await _context.Clients
            .AsNoTracking()
            .Select(client => new ClientProductCountDto
            {
                ClientName = client.Name,
                TotalProducts = client.Orders
                    .SelectMany(order => order.Orderdetails)
                    .Sum(detail => detail.Quantity)
            })
            .ToListAsync();

        return clientsWithCount;
    }
    
    public async Task<IEnumerable<SalesByClientDto>> GetSalesByClientAsync()
    {
        var salesReport = await _context.Orders
            .AsNoTracking()
            .Include(order => order.Orderdetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .GroupBy(order => order.Clientid)
            .Select(group => new SalesByClientDto
            {
                ClientName = _context.Clients
                    .FirstOrDefault(c => c.Clientid == group.Key)!.Name,
                TotalSales = group.Sum(order => order.Orderdetails
                    .Sum(detail => detail.Quantity * detail.Product.Price))
            })
            .OrderByDescending(s => s.TotalSales)
            .ToListAsync();

        return salesReport;
    }

}