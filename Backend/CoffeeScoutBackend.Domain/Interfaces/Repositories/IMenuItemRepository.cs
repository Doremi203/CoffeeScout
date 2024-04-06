using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IMenuItemRepository
{
    Task<MenuItem?> GetById(long id);
    Task<IReadOnlyCollection<MenuItem>> GetAllInAreaByBeverageType(
        Location location, double radiusInMeters, BeverageType beverageType);
    Task Add(MenuItem menuItem);
}