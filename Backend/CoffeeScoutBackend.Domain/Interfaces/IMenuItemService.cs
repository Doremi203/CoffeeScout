using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface IMenuItemService
{
    Task<MenuItem> GetByIdAsync(long id);
    Task<BeverageType> GetBeverageTypeByNameAsync(string name);
    Task AddAsync(MenuItem menuItem);
    Task AddBeverageTypeAsync(BeverageType beverageType);
}