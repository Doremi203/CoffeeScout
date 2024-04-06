using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class MenuItemService(
    IMenuItemRepository menuItemRepository,
    IBeverageTypeService beverageTypeService
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
        var beverageType = await beverageTypeService.GetBeverageTypeByIdAsync(beverageTypeId);

        return await menuItemRepository.GetAllInAreaByBeverageTypeAsync(
            location, radiusInMeters, beverageType);
    }

    public async Task AddAsync(MenuItem menuItem)
    {
        await menuItemRepository.AddAsync(menuItem);
    }
}