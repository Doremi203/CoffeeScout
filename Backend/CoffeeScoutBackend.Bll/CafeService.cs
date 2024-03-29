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
        var cafe = await cafeRepository.GetByAdminIdAsync(adminId);

        var newMenuItem = new MenuItem
        {
            Name = menuItem.Name,
            Price = menuItem.Price,
            BeverageType = beverageType,
            Cafe = cafe
        };

        await menuItemRepository.AddAsync(newMenuItem);
    }

    public async Task AssignNewCafeAdminAsync(string adminId, long cafeId)
    {
        var cafe = await cafeRepository.GetByIdAsync(cafeId)
                   ?? throw new CafeNotFoundException(
                       $"Cafe with id {cafeId} not found",
                       cafeId);

        await cafeRepository.CreateCafeAdminAsync(new CafeAdmin
        {
            UserId = adminId,
            Cafe = cafe
        });
    }
}