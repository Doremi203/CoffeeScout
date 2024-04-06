using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<long> CreateOrder(CreateOrderData orderData);
    Task<IReadOnlyCollection<Order>> GetCafeOrders(
        string currentCafeAdminId, OrderStatus status, DateTime from);
}