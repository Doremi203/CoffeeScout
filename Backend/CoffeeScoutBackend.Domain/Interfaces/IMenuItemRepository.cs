using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface IMenuItemRepository
{
    Task<MenuItem?> GetByIdAsync(long id);
    Task<BeverageType?> GetBeverageTypeByNameAsync(string name);
    Task AddAsync(MenuItem menuItem);
}