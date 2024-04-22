using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class CustomerRepository(
    AppDbContext dbContext
) : ICustomerRepository
{
    public async Task<Customer?> GetById(string userId)
    {
        var customer = await dbContext.Customers
            .Include(c => c.User)
            .Include(c => c.FavoriteMenuItems)
            .ThenInclude(mi => mi.BeverageType)
            .Include(c => c.FavoriteMenuItems)
            .ThenInclude(mi => mi.Cafe)
            .FirstOrDefaultAsync(c => c.Id == userId);

        return customer?.Adapt<Customer>();
    }

    public async Task Add(Customer customer)
    {
        await dbContext.Customers.AddAsync(customer.Adapt<CustomerEntity>());
        await dbContext.SaveChangesAsync();
    }

    public async Task AddFavoredMenuItem(Customer customer, MenuItem menuItem)
    {
        var customerEntity = await dbContext.Customers
            .Include(c => c.FavoriteMenuItems)
            .FirstAsync(c => c.Id == customer.Id);
        var menuItemEntity = await dbContext.MenuItems
            .FirstAsync(mi => mi.Id == menuItem.Id);

        customerEntity.FavoriteMenuItems.Add(menuItemEntity);

        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveFavoredMenuItem(Customer customer, MenuItem menuItem)
    {
        var customerEntity = await dbContext.Customers
            .Include(c => c.FavoriteMenuItems)
            .FirstAsync(c => c.Id == customer.Id);
        var menuItemEntity = await dbContext.MenuItems
            .FirstAsync(mi => mi.Id == menuItem.Id);

        customerEntity.FavoriteMenuItems.Remove(menuItemEntity);

        await dbContext.SaveChangesAsync();
    }

    public async Task Update(Customer customer)
    {
        var customerEntity = await dbContext.Customers
            .FirstAsync(c => c.Id == customer.Id);

        customerEntity.FirstName = customer.FirstName;

        dbContext.Customers.Update(customerEntity);
        await dbContext.SaveChangesAsync();
    }
}