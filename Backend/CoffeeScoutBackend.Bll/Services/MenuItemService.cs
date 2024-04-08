using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class MenuItemService(
    IMenuItemRepository menuItemRepository,
    IBeverageTypeService beverageTypeService,
    ICafeService cafeService
) : IMenuItemService
{
    public async Task<MenuItem> GetById(long id)
    {
        return await menuItemRepository.GetById(id)
               ?? throw new MenuItemNotFoundException(
                   $"Menu item with id:{id} not found",
                   id);
    }

    public async Task<IEnumerable<MenuItem>> GetAllInAreaByBeverageType(
        Location location,
        double radiusInMeters,
        long beverageTypeId
    )
    {
        var beverageType = await beverageTypeService.GetBeverageTypeById(beverageTypeId);

        return await menuItemRepository.GetAllInAreaByBeverageType(
            location, radiusInMeters, beverageType);
    }
    
    public async Task<MenuItem> Add(string adminId, MenuItem menuItem)
    {
        var beverageName = menuItem.BeverageType.Name;
        var beverageType = await beverageTypeService.GetBeverageTypeByNameAsync(beverageName);
        var cafe = await cafeService.GetByAdminId(adminId);

        var newMenuItem = new MenuItem
        {
            Name = menuItem.Name,
            Price = menuItem.Price,
            BeverageType = beverageType,
            Cafe = cafe
        };

        return await menuItemRepository.Add(newMenuItem);
    }
}