using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order> Add(Order order);
    Task<Order?> GetById(long orderId);
    Task<IReadOnlyCollection<Order>> GetByUserId(string userId, OrderStatus status, DateTime from);
    Task<IReadOnlyCollection<Order>> GetByCafeId(long cafeId, OrderStatus status, DateTime from);
    Task UpdateOrderItemCompletionStatus(long orderId, long menuItemId, bool isCompleted);
    Task UpdateStatus(long id, OrderStatus cancelled);
}