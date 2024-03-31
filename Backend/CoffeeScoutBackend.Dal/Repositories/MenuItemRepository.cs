using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Dal.Infrastructure;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Location = CoffeeScoutBackend.Domain.Models.Location;

namespace CoffeeScoutBackend.Dal.Repositories;

public class MenuItemRepository(
    AppDbContext dbContext,
    ILocationProvider locationProvider
) : IMenuItemRepository
{
    public async Task<MenuItem?> GetByIdAsync(long id)
    {
        var menuItem = await dbContext.MenuItems
            .Include(m => m.BeverageType)
            .Include(m => m.Cafe)
            .FirstOrDefaultAsync(m => m.Id == id);
        return menuItem?.Adapt<MenuItem>();
    }

    public async Task<BeverageType?> GetBeverageTypeByNameAsync(string name)
    {
        var beverageType = await dbContext.BeverageTypes
            .FirstOrDefaultAsync(bt => bt.Name == name);
        return beverageType?.Adapt<BeverageType>();
    }

    public async Task<BeverageType?> GetBeverageTypeByIdAsync(long id)
    {
        var beverageType = await dbContext.BeverageTypes
            .FirstOrDefaultAsync(bt => bt.Id == id);
        return beverageType?.Adapt<BeverageType>();
    }

    public async Task<IReadOnlyCollection<MenuItem>> GetAllInAreaByBeverageTypeAsync(
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

    public async Task AddAsync(MenuItem menuItem)
    {
        var entity = menuItem.Adapt<MenuItemEntity>();
        entity.BeverageType = await dbContext.BeverageTypes
            .FirstAsync(bt => bt.Id == entity.BeverageTypeId);
        entity.Cafe = await dbContext.Cafes
            .FirstAsync(c => c.Id == entity.CafeId);

        await dbContext.MenuItems.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddBeverageTypeAsync(BeverageType beverageType)
    {
        var entity = beverageType.Adapt<BeverageTypeEntity>();
        await dbContext.BeverageTypes.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }
}