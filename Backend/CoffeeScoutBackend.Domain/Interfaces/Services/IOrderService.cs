using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<long> CreateOrderAsync(CreateOrderData orderData);
}