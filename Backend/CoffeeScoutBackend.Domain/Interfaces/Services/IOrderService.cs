using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateOrder(CreateOrderData orderData);
    Task<Order> GetById(long id);
    Task<IReadOnlyCollection<Order>> GetCafeOrders(
        string currentCafeAdminId, OrderStatus status, DateTime from);

    Task<IReadOnlyCollection<Order>> GetCustomerOrders(
        string userId, OrderStatus status, DateTime from);

    Task CompleteCafeOrderPart(string adminId, long id);
    Task CancelOrder(long id);
}