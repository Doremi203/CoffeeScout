using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

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
        long beverageTypeId
    )
    {
        var beverageType = await GetBeverageTypeByIdAsync(beverageTypeId);

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

    public async Task<BeverageType> GetBeverageTypeByIdAsync(long id)
    {
        return await menuItemRepository
                   .GetBeverageTypeByIdAsync(id)
               ?? throw new BeverageTypeNotFoundException(
                   $"Beverage type with id {id} not found",
                   id);
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