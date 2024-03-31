using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class OrderService : IOrderService
{
    public Task<long> CreateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }
}