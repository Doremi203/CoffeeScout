using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class CustomerRepository(
    AppDbContext dbContext
) : ICustomerRepository
{
    public async Task<Customer?> GetByIdAsync(string userId)
    {
        var customer = await dbContext.Customers
            .Include(c => c.User)
            .Include(c => c.FavoriteMenuItems)
            .ThenInclude(mi => mi.BeverageTypeEntity)
            .FirstOrDefaultAsync(c => c.UserId == userId);
        return customer?.Adapt<Customer>();
    }

    public async Task AddAsync(Customer customer)
    {
        await dbContext.Customers.AddAsync(customer.Adapt<CustomerEntity>());
        await dbContext.SaveChangesAsync();
    }

    public async Task AddFavoredMenuItemAsync(Customer customer, MenuItem menuItem)
    {
        var customerEntity = await dbContext.Customers
            .Include(c => c.FavoriteMenuItems)
            .FirstOrDefaultAsync(c => c.UserId == customer.UserId);
        var menuItemEntity = await dbContext.MenuItems.FindAsync(menuItem.Id);
        if (customerEntity is null || menuItemEntity is null)
            return;

        customerEntity.FavoriteMenuItems.Add(menuItemEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BeverageType>> GetFavoredBeverageTypesAsync(string userId)
    {
        var customer = await dbContext.Customers
            .Include(c => c.FavoriteMenuItems)
            .ThenInclude(mi => mi.BeverageTypeEntity)
            .FirstOrDefaultAsync(c => c.UserId == userId);
        if (customer is null)
            return Enumerable.Empty<BeverageType>();

        var beverageTypes =
            customer.FavoriteMenuItems
                .Select(mi => mi.BeverageTypeEntity)
                .Distinct();

        return beverageTypes.Adapt<IEnumerable<BeverageType>>();
    }

    public Task UpdateAsync(string userId, Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string userId)
    {
        throw new NotImplementedException();
    }
}