using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.Domain.ServiceModels;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateOrder(CreateOrderData orderData);
    Task<Order> GetById(long id);
    Task<IReadOnlyCollection<Order>> GetCafeOrders(string adminId, GetOrdersModel model);
    Task<IReadOnlyCollection<Order>> GetCustomerOrders(string userId, GetOrdersModel model);
    Task CompleteOrder(string adminId, long id);
    Task CafeCancelOrder(string adminId, long id);
    Task CustomerCancelOrder(string userId, long id);
    Task PayOrder(string getId, long id);
}