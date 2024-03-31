using System.Transactions;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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
            .FirstAsync(c => c.Id == order.Customer.Id);

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
        var orderEntity = await GetOrderEntities()
            .FirstOrDefaultAsync(o => o.Id == orderId);

        return orderEntity?.Adapt<Order>();
    }

    public async Task<IReadOnlyCollection<Order>> GetByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<Order>> GetOrdersAsync(OrderStatus status, DateTime from)
    {
        var orderEntities = await GetOrderEntities()
            .Where(o => o.Status == status && o.OrderDate >= from)
            .ToListAsync();

        return orderEntities.Adapt<IReadOnlyCollection<Order>>();
    }

    private IIncludableQueryable<OrderEntity, CustomerEntity> GetOrderEntities()
    {
        return dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .ThenInclude(mi => mi.Cafe)
            .Include(o => o.Customer);
    }
}