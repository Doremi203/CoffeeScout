using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IMenuItemRepository
{
    Task<MenuItem?> GetByIdAsync(long id);
    Task<BeverageType?> GetBeverageTypeByNameAsync(string name);
    Task<BeverageType?> GetBeverageTypeByIdAsync(long id);
    Task<IReadOnlyCollection<MenuItem>> GetAllInAreaByBeverageTypeAsync(
        Location location, double radiusInMeters, BeverageType beverageType);
    Task AddAsync(MenuItem menuItem);
    Task AddBeverageTypeAsync(BeverageType beverageType);
}