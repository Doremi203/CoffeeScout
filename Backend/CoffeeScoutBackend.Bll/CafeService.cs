using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll;

public class CafeService(
    IMenuItemRepository menuItemRepository,
    ICafeRepository cafeRepository
) : ICafeService
{
    public async Task AddMenuItemAsync(string adminId, MenuItem menuItem)
    {
        var beverageName = menuItem.BeverageType.Name;
        var beverageType = await menuItemRepository
                               .GetBeverageTypeByNameAsync(beverageName)
                           ?? throw new BeverageTypeNotFoundException(
                               $"Beverage type with name {beverageName} not found",
                               beverageName);
        var cafe = await cafeRepository.GetCafeByAdminIdAsync(adminId);

        var newMenuItem = new MenuItem
        {
            Name = menuItem.Name,
            Price = menuItem.Price,
            BeverageType = beverageType,
            Cafe = cafe
        };

        await menuItemRepository.AddAsync(newMenuItem);
    }
}