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
        var menuItem = await dbContext.MenuItems
            .Include(m => m.BeverageType)
            .Include(m => m.Cafe)
            .FirstOrDefaultAsync(m => m.Id == id);
        return menuItem?.Adapt<MenuItem>();
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
        var entity = menuItem.Adapt<MenuItemEntity>();
        entity.BeverageType = await dbContext.BeverageTypes
            .FirstAsync(bt => bt.Id == entity.BeverageTypeId);
        entity.Cafe = await dbContext.Cafes
            .FirstAsync(c => c.Id == entity.CafeId);

        await dbContext.MenuItems.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        
        return entity.Adapt<MenuItem>();
    }
}