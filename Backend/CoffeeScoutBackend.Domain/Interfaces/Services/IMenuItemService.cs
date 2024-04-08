using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IMenuItemService
{
    Task<MenuItem> GetById(long id);
    Task<IReadOnlyCollection<MenuItem>> GetAllInAreaByBeverageType(
        Location location, double radiusInMeters, long beverageTypeId);
    Task<MenuItem> Add(string cafeAdminId, MenuItem menuItem);
    Task Update(long id, MenuItem menuItem);
    Task<IReadOnlyCollection<MenuItem>> GetCafeMenuItems(string cafeAdminId);
}