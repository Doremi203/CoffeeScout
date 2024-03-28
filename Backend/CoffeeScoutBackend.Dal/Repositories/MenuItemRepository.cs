using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class MenuItemRepository(
    AppDbContext dbContext
) : IMenuItemRepository
{
    public async Task<MenuItem?> GetByIdAsync(long id)
    {
        var menuItem = await dbContext.MenuItems.FindAsync(id);
        return menuItem?.Adapt<MenuItem>();
    }

    public async Task<BeverageType?> GetBeverageTypeByNameAsync(string name)
    {
        var beverageType = await dbContext.BeverageTypes
            .FirstOrDefaultAsync(bt => bt.Name == name);
        return beverageType?.Adapt<BeverageType>();
    }

    public async Task AddAsync(MenuItem menuItem)
    {
        var entity = menuItem.Adapt<MenuItemEntity>();
        await dbContext.MenuItems.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }
}