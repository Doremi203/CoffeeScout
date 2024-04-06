using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IBeverageTypeService
{
    Task<BeverageType> GetBeverageTypeByNameAsync(string name);
    Task<BeverageType> GetBeverageTypeByIdAsync(long id);
    Task AddBeverageTypeAsync(BeverageType beverageType);
    Task UpdateBeverageTypeNameAsync(long id, string name);
}