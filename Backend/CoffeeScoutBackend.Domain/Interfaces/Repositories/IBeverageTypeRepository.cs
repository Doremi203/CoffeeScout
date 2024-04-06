using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IBeverageTypeRepository
{
    Task<BeverageType?> GetBeverageTypeByNameAsync(string name);
    Task<BeverageType?> GetBeverageTypeByIdAsync(long id);
    Task AddBeverageTypeAsync(BeverageType beverageType);
    
    Task UpdateBeverageTypeAsync(BeverageType beverageType);
}