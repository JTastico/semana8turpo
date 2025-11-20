using Lab09.DTOs;
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
    

    public async Task<IEnumerable<OrderDetailsDto>> GetAllOrdersWithDetailsAsync()
    {
        var ordersWithDetails = await _context.Orders
            .Include(order => order.Orderdetails)
                .ThenInclude(orderDetail => orderDetail.Product)
            .AsNoTracking()
            .Select(order => new OrderDetailsDto
            {
                OrderId = order.Orderid,
                OrderDate = order.Orderdate,
                Products = order.Orderdetails
                    .Select(od => new ProductDto
                    {
                        ProductName = od.Product.Name,
                        Quantity = od.Quantity,
                        Price = od.Product.Price
                    }).ToList()
            })
            .ToListAsync();
        
        return ordersWithDetails;
    }
}