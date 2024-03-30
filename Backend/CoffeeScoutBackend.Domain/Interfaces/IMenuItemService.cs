using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface IMenuItemService
{
    Task<MenuItem> GetByIdAsync(long id);
    Task<IEnumerable<MenuItem>> GetAllInAreaByBeverageTypeAsync(
        Location location, double radiusInMeters, string beverageTypeName);
    Task<BeverageType> GetBeverageTypeByNameAsync(string name);
    Task AddAsync(MenuItem menuItem);
    Task AddBeverageTypeAsync(BeverageType beverageType);
}