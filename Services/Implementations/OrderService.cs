using Lab8_JamilTurpo.Data;
using Lab8_JamilTurpo.DTOs;
using Lab8_JamilTurpo.Models;
using Lab8_JamilTurpo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8_JamilTurpo.Services;

public class OrderService : IOrderService
{
    private readonly LINQExampleContext _context;

    public OrderService(LINQExampleContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDetailDto>> GetOrderDetailsByIdAsync(int orderId)
    {
        var orderDetails = await _context.Orderdetails 
            .Include(od => od.Product) 
            .Where(od => od.Orderid == orderId) 
            .Select(od => new OrderDetailDto 
            {
                ProductName = od.Product.Name, 
                Quantity = od.Quantity 
            })
            .ToListAsync(); 

        return orderDetails;
    }
    public async Task<int?> GetTotalProductQuantityByOrderIdAsync(int orderId)
    {
        var orderExists = await _context.Orders.AnyAsync(o => o.Orderid == orderId);
        if (!orderExists)
        {
            return null;
        }
        
        int totalQuantity = await _context.Orderdetails
            .Where(od => od.Orderid == orderId)
            .SumAsync(od => od.Quantity);

        return totalQuantity;
    }
    
    public async Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date)
    {
        var orders = await _context.Orders
            .Where(o => o.Orderdate > date)
            .ToListAsync();

        return orders;
    }
    
    public async Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync()
    {
        var ordersWithDetails = await _context.Orders
            .Include(o => o.Orderdetails) 
            .ThenInclude(od => od.Product) 
            .Select(o => new OrderWithDetailsDto
            {
                OrderId = o.Orderid,
                OrderDate = o.Orderdate,
                ClientId = o.Clientid,
                Details = o.Orderdetails.Select(od => new OrderDetailDto 
                {
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity
                }).ToList()
            })
            .ToListAsync(); 
        return ordersWithDetails;
    }
}