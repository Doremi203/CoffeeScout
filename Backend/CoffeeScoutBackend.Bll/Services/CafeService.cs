using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class CafeService(
    IMenuItemService menuItemService,
    ICafeRepository cafeRepository
) : ICafeService
{
    public async Task<Cafe> GetByIdAsync(long id)
    {
        return await cafeRepository.GetByIdAsync(id)
               ?? throw new CafeNotFoundException(
                   $"Cafe with id {id} not found",
                   id);
    }

    public async Task<Cafe> GetByAdminIdAsync(string adminId)
    {
        return await cafeRepository.GetByAdminIdAsync(adminId)
               ?? throw new CafeNotFoundException(
                   $"Cafe for admin with id: {adminId} not found");
    }

    public async Task AddMenuItemAsync(string adminId, MenuItem menuItem)
    {
        var beverageName = menuItem.BeverageType.Name;
        var beverageType = await menuItemService.GetBeverageTypeByNameAsync(beverageName);
        var cafe = await GetByAdminIdAsync(adminId);

        var newMenuItem = new MenuItem
        {
            Name = menuItem.Name,
            Price = menuItem.Price,
            BeverageType = beverageType,
            Cafe = cafe
        };

        await menuItemService.AddAsync(newMenuItem);
    }

    public async Task AssignNewCafeAdminAsync(string adminId, long cafeId)
    {
        var cafe = await GetByIdAsync(cafeId);
        await cafeRepository.CreateCafeAdminAsync(new CafeAdmin
        {
            UserId = adminId,
            Cafe = cafe
        });
    }
}