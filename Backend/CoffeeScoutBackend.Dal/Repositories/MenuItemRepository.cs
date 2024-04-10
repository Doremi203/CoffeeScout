using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Dal.Infrastructure;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class MenuItemRepository(
    AppDbContext dbContext,
    ILocationProvider locationProvider
) : IMenuItemRepository
{
    public async Task<MenuItem?> GetById(long id)
    {
        var menuItemEntity = await dbContext.MenuItems
            .Include(m => m.BeverageType)
            .Include(m => m.Cafe)
            .FirstOrDefaultAsync(m => m.Id == id);
        return menuItemEntity?.Adapt<MenuItem>();
    }

    public async Task<IReadOnlyCollection<MenuItem>> GetAllInAreaByBeverageType(
        Location location,
        double radiusInMeters,
        BeverageType beverageType)
    {
        var area = locationProvider.CreateArea(location, radiusInMeters);

        var menuItems = await dbContext.MenuItems
            .Include(m => m.BeverageType)
            .Include(m => m.Cafe)
            .Where(m => m.Cafe.Location.Within(area) && m.BeverageTypeId == beverageType.Id)
            .ToListAsync();

        return menuItems.Adapt<IReadOnlyCollection<MenuItem>>();
    }

    public async Task<MenuItem> Add(MenuItem menuItem)
    {
        var menuItemEntity = menuItem.Adapt<MenuItemEntity>();
        menuItemEntity.BeverageType = await dbContext.BeverageTypes
            .FirstAsync(bt => bt.Id == menuItemEntity.BeverageTypeId);
        menuItemEntity.Cafe = await dbContext.Cafes
            .FirstAsync(c => c.Id == menuItemEntity.CafeId);

        dbContext.MenuItems.Add(menuItemEntity);
        await dbContext.SaveChangesAsync();

        return menuItemEntity.Adapt<MenuItem>();
    }

    public async Task Update(MenuItem menuItem)
    {
        var menuItemEntity = await dbContext.MenuItems
            .FirstAsync(m => m.Id == menuItem.Id);
        
        menuItemEntity.BeverageType = await dbContext.BeverageTypes
            .FirstAsync(bt => bt.Id == menuItemEntity.BeverageTypeId);
        menuItemEntity.Name = menuItem.Name;
        menuItemEntity.Price = menuItem.Price;
        menuItemEntity.SizeInMl = menuItem.SizeInMl;

        dbContext.MenuItems.Update(menuItemEntity);

        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        await dbContext.MenuItems
            .Where(mi => mi.Id == id)
            .ExecuteDeleteAsync();

        await dbContext.SaveChangesAsync();
    }
}