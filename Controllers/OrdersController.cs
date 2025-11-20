using Lab8_JamilTurpo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Lab8_JamilTurpo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET: api/Orders/1/details
    [HttpGet("{orderId}/details")]
    public async Task<IActionResult> GetOrderDetails(int orderId)
    {
        var details = await _orderService.GetOrderDetailsByIdAsync(orderId);

        if (details == null || !details.Any())
        {
            return NotFound($"No se encontraron detalles para la orden con ID {orderId}.");
        }

        return Ok(details);
    }
    
    [HttpGet("{orderId}/total-quantity")]
    public async Task<IActionResult> GetTotalProductQuantity(int orderId)
    {
        var total = await _orderService.GetTotalProductQuantityByOrderIdAsync(orderId);

        if (total == null)
        {
            return NotFound($"No se encontró la orden con ID {orderId}.");
        }
        
        return Ok(new { orderId = orderId, totalProductQuantity = total });
    }
    
    [HttpGet("after-date/{date}")]
    public async Task<IActionResult> GetOrdersAfterDate(DateTime date)
    {
        var orders = await _orderService.GetOrdersAfterDateAsync(date);

        if (orders == null || !orders.Any())
        {
            return NotFound($"No se encontraron órdenes después de la fecha {date:yyyy-MM-dd}.");
        }

        return Ok(orders);
    }
    
    [HttpGet("with-details")]
    public async Task<IActionResult> GetOrdersWithDetails()
    {
        var orders = await _orderService.GetAllOrdersWithDetailsAsync();
        
        if (orders == null || !orders.Any())
        {
            return NotFound("No se encontraron órdenes.");
        }

        return Ok(orders);
    }
}