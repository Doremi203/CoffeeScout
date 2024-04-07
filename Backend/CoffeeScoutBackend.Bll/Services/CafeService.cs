using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class CafeService(
    IMenuItemService menuItemService,
    IBeverageTypeService beverageTypeService,
    ICafeRepository cafeRepository
) : ICafeService
{
    public async Task<Cafe> GetById(long id)
    {
        return await cafeRepository.GetById(id)
               ?? throw new CafeNotFoundException(
                   $"Cafe with id {id} not found",
                   id);
    }

    public async Task<Cafe> GetByAdminId(string adminId)
    {
        return await cafeRepository.GetByAdminId(adminId)
               ?? throw new CafeNotFoundException(
                   $"Cafe for admin with id: {adminId} not found");
    }

    public async Task AddMenuItem(string adminId, MenuItem menuItem)
    {
        var beverageName = menuItem.BeverageType.Name;
        var beverageType = await beverageTypeService.GetBeverageTypeByNameAsync(beverageName);
        var cafe = await GetByAdminId(adminId);

        var newMenuItem = new MenuItem
        {
            Name = menuItem.Name,
            Price = menuItem.Price,
            BeverageType = beverageType,
            Cafe = cafe
        };

        await menuItemService.Add(newMenuItem);
    }

    public async Task AssignNewCafeAdmin(string adminId, long cafeId)
    {
        var cafe = await GetById(cafeId);
        await cafeRepository.CreateCafeAdmin(new CafeAdmin
        {
            Id = adminId,
            Cafe = cafe
        });
    }

    public async Task<IReadOnlyCollection<Cafe>> GetCafesInArea(Location location, double radius)
    {
        return await cafeRepository.GetCafesInArea(location, radius);
    }

    public async Task AddCafe(Cafe cafe)
    {
        await cafeRepository.Add(cafe);
    }
}