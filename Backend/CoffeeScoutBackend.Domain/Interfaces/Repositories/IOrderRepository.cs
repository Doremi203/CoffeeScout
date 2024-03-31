using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<long> AddAsync(Order order);
    Task<Order?> GetByIdAsync(long orderId);
    Task<IReadOnlyCollection<Order>> GetByUserIdAsync(string userId);
    Task<IReadOnlyCollection<Order>> GetOrdersAsync(OrderStatus status, DateTime from);
}