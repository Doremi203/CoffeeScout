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
    public async Task<Order> Add(Order order)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var orderEntity = order.Adapt<OrderEntity>();
        
        orderEntity.Cafe = await dbContext.Cafes
            .FirstAsync(c => c.Id == order.Cafe.Id);

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
        return addedOrder.Entity.Adapt<Order>();
    }

    public async Task<Order?> GetById(long orderId)
    {
        var orderEntity = await GetOrderEntities()
            .FirstOrDefaultAsync(o => o.Id == orderId);

        return orderEntity?.Adapt<Order>();
    }

    public async Task<IReadOnlyCollection<Order>> GetByUserId(string userId, OrderStatus status, DateTime from)
    {
        var orderEntities = await GetOrderEntities()
            .Where(o => 
                o.Customer.Id == userId 
                && o.Status == status 
                && o.Date >= from)
            .ToListAsync();

        return orderEntities.Adapt<IReadOnlyCollection<Order>>();
    }

    public async Task<IReadOnlyCollection<Order>> GetByCafeId(long cafeId, OrderStatus status, DateTime from)
    {
        var orderEntities = await GetOrderEntities()
            .Where(o => 
                o.Status == status 
                && o.Date >= from 
                && o.OrderItems.Any(oi => oi.MenuItem.Cafe.Id == cafeId))
            .ToListAsync();

        return orderEntities.Adapt<IReadOnlyCollection<Order>>();
    }

    public async Task UpdateStatus(long id, OrderStatus cancelled)
    {
        var orderEntity = await dbContext.Orders
            .FirstAsync(o => o.Id == id);

        orderEntity.Status = cancelled;

        dbContext.Update(orderEntity);

        await dbContext.SaveChangesAsync();
    }

    private IIncludableQueryable<OrderEntity, CustomerEntity> GetOrderEntities()
    {
        return dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .Include(o => o.Cafe)
            .Include(o => o.Customer);
    }
}