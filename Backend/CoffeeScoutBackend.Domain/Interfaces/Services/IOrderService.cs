using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<long> CreateOrderAsync(CreateOrderData orderData);
    Task<IReadOnlyCollection<Order>> GetCafeOrdersAsync(
        string currentCafeAdminId, OrderStatus status, DateTime from);
}