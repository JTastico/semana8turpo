using Lab8_JamilTurpo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab8_JamilTurpo.Models;

namespace Lab8_JamilTurpo.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDetailDto>> GetOrderDetailsByIdAsync(int orderId);
    Task<int?> GetTotalProductQuantityByOrderIdAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date);
    Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync();
}