using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll;

public class CafeService(
    IMenuItemRepository menuItemRepository
) : ICafeService
{
    public async Task AddMenuItemAsync(MenuItem menuItem)
    {
        var beverageName = menuItem.BeverageType.Name;
        var beverageType = await menuItemRepository
                               .GetBeverageTypeByNameAsync(beverageName)
                           ?? throw new BeverageTypeNotFoundException(
                               $"Beverage type with name {beverageName} not found",
                               beverageName);

        var newMenuItem = new MenuItem
        {
            Name = menuItem.Name,
            Price = menuItem.Price,
            BeverageType = beverageType
        };

        await menuItemRepository.AddAsync(newMenuItem);
    }
}