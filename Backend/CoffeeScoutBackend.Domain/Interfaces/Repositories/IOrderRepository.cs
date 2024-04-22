using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order> Add(Order order);
    Task<Order?> GetById(long orderId);
    Task<IReadOnlyCollection<Order>> GetByUserId(string userId, GetOrdersModel model);
    Task<IReadOnlyCollection<Order>> GetByCafeId(long cafeId, GetOrdersModel model);
    Task UpdateStatus(long id, OrderStatus status);
}