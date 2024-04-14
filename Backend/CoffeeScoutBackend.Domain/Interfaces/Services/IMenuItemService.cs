using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IMenuItemService
{
    Task<MenuItem> GetById(long id);
    Task<IReadOnlyCollection<MenuItem>> GetByCafeId(long id);
    Task<IReadOnlyCollection<MenuItem>> GetAllInAreaByBeverageType(
        Location location, double radiusInMeters, long beverageTypeId);
    Task<MenuItem> Add(AddMenuItemModel model);
    Task Update(UpdateMenuItemModel model);
    Task<IReadOnlyCollection<MenuItem>> GetByCafeAdmin(string cafeAdminId);
    Task Delete(long id);
    Task<IReadOnlyCollection<MenuItem>> Search(string name, int limit);
}