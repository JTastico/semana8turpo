using Lab8_JamilTurpo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab8_JamilTurpo.Data;

namespace Lab8_JamilTurpo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly LINQExampleContext _context;

    public ClientsController(IClientService clientService, LINQExampleContext context)
    {
        _clientService = clientService;
        _context = context;
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> GetClientsByName([FromQuery] string name)
    {
        var clients = await _clientService.GetClientsByNameAsync(name);

        if (clients == null || !clients.Any())
        {
            return NotFound("No se encontraron clientes con ese nombre.");
        }

        return Ok(clients);
    }
    
    [HttpGet("with-most-orders")]
    public async Task<IActionResult> GetClientWithMostOrders()
    {
        var client = await _clientService.GetClientWithMostOrdersAsync();

        if (client == null)
        {
            return NotFound("No se encontraron clientes o no hay órdenes en la base de datos.");
        }

        return Ok(client);
    }
    
    
    [HttpGet("{clientId}/products")]
    public async Task<IActionResult> GetProductsPurchasedByClient(int clientId)
    {
        var clientExists = await _context.Clients.AnyAsync(c => c.Clientid == clientId);
        if (!clientExists)
        {
            return NotFound($"No se encontró el cliente con ID {clientId}.");
        }

        var products = await _clientService.GetProductsPurchasedByClientAsync(clientId);

        if (products == null || !products.Any())
        {
            return NotFound($"El cliente con ID {clientId} no ha comprado ningún producto.");
        }

        return Ok(products);
    }
    
    [HttpGet("by-product/{productId}")]
    public async Task<IActionResult> GetClientsByProduct(int productId)
    {
        // Verificamos si el producto existe para dar una respuesta 404 más clara.
        var productExists = await _context.Products.AnyAsync(p => p.Productid == productId);
        if (!productExists)
        {
            return NotFound($"No se encontró el producto con ID {productId}.");
        }

        var clients = await _clientService.GetClientsByProductAsync(productId);

        if (clients == null || !clients.Any())
        {
            return NotFound($"Ningún cliente ha comprado el producto con ID {productId}.");
        }

        return Ok(clients);
    }
}