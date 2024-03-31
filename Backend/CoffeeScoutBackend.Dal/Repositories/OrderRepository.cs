using System.Transactions;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class OrderRepository(
    AppDbContext dbContext
) : IOrderRepository
{
    public async Task<long> AddAsync(Order order)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var orderEntity = order.Adapt<OrderEntity>();
        
        orderEntity.Customer = await dbContext.Customers
            .FirstAsync(c => c.UserId == order.Customer.UserId);
         
        orderEntity.OrderItems = orderEntity.OrderItems.Select(oi =>
        {
            oi.MenuItem = dbContext.MenuItems
                .First(mi => mi.Id == oi.MenuItem.Id);
            return oi;
        }).ToList();
        
        var addedOrder = await dbContext.Orders.AddAsync(orderEntity);
        await dbContext.SaveChangesAsync();
        transaction.Complete();
        return addedOrder.Entity.Id;
    }

    public async Task<Order?> GetByIdAsync(long orderId)
    {
        var orderEntity = await dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .ThenInclude(mi => mi.Cafe)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        
        return orderEntity?.Adapt<Order>();
    }

    public async Task<IReadOnlyCollection<Order>> GetByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }
}