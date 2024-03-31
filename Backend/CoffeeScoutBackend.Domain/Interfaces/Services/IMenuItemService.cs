using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IMenuItemService
{
    Task<MenuItem> GetByIdAsync(long id);
    Task<IEnumerable<MenuItem>> GetAllInAreaByBeverageTypeAsync(
        Location location, double radiusInMeters, long beverageTypeId);
    Task<BeverageType> GetBeverageTypeByNameAsync(string name);
    Task<BeverageType> GetBeverageTypeByIdAsync(long id);
    Task AddAsync(MenuItem menuItem);
    Task AddBeverageTypeAsync(BeverageType beverageType);
}