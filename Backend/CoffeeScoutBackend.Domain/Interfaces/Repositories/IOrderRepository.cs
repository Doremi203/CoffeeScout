using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<long> Add(Order order);
    Task<Order?> GetById(long orderId);
    Task<IReadOnlyCollection<Order>> GetByUserId(string userId);
    Task<IReadOnlyCollection<Order>> GetOrders(OrderStatus status, DateTime from);
}