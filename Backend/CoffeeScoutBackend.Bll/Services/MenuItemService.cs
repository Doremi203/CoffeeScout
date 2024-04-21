using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;
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

    public async Task<IReadOnlyCollection<MenuItem>> GetByCafeId(long id)
    {
        var cafe = await cafeService.GetById(id);

        return cafe.MenuItems;
    }

    public async Task<IReadOnlyCollection<MenuItem>> GetAllInAreaByBeverageType(
        Location location,
        double radiusInMeters,
        long beverageTypeId
    )
    {
        var beverageType = await beverageTypeService.GetById(beverageTypeId);

        return await menuItemRepository.GetAllInAreaByBeverageType(
            location, radiusInMeters, beverageType);
    }
    
    public async Task<MenuItem> Add(AddMenuItemModel model)
    {
        var beverageType = await beverageTypeService.GetById(model.BeverageTypeId);
        var cafe = await cafeService.GetByAdminId(model.CafeAdminId);

        var newMenuItem = new MenuItem
        {
            Name = model.Name,
            Price = model.Price,
            BeverageType = beverageType,
            Cafe = cafe,
            SizeInMl = model.SizeInMl
        };

        return await menuItemRepository.Add(newMenuItem);
    }

    public async Task Update(UpdateMenuItemModel model)
    {
        var existingMenuItem = await GetById(model.Id);
        var beverageType = await beverageTypeService.GetById(model.BeverageTypeId);

        var newMenuItem = existingMenuItem with
        {
            Name = model.Name,
            Price = model.Price,
            SizeInMl = model.SizeInMl,
            BeverageType = beverageType
        };

        await menuItemRepository.Update(newMenuItem);
    }

    public async Task<IReadOnlyCollection<MenuItem>> GetByCafeAdmin(string cafeAdminId)
    {
        var cafe = await cafeService.GetByAdminId(cafeAdminId);

        return cafe.MenuItems;
    }

    public async Task Delete(long id)
    {
        var menuItem = await GetById(id);

        await menuItemRepository.Delete(menuItem.Id);
    }

    public async Task<IReadOnlyCollection<MenuItem>> Search(string name, int limit)
    {
        return await menuItemRepository.Search(name, limit);
    }
}