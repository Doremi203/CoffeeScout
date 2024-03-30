using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll;

public class MenuItemService(
    IMenuItemRepository menuItemRepository
) : IMenuItemService
{
    public async Task<MenuItem> GetByIdAsync(long id)
    {
        return await menuItemRepository.GetByIdAsync(id)
               ?? throw new MenuItemNotFoundException(
                   $"Menu item with id:{id} not found",
                   id);
    }

    public async Task<IEnumerable<MenuItem>> GetAllInAreaByBeverageTypeAsync(
        Location location,
        double radiusInMeters,
        string beverageTypeName
    )
    {
        var beverageType = await GetBeverageTypeByNameAsync(beverageTypeName);
        
        return await menuItemRepository.GetAllInAreaByBeverageTypeAsync(
            location, radiusInMeters, beverageType);
    }

    public async Task<BeverageType> GetBeverageTypeByNameAsync(string name)
    {
        return await menuItemRepository
                   .GetBeverageTypeByNameAsync(name)
               ?? throw new BeverageTypeNotFoundException(
                   $"Beverage type with name {name} not found",
                   name);
    }

    public async Task AddAsync(MenuItem menuItem)
    {
        await menuItemRepository.AddAsync(menuItem);
    }

    public Task AddBeverageTypeAsync(BeverageType beverageType)
    {
        return menuItemRepository.AddBeverageTypeAsync(beverageType);
    }
}