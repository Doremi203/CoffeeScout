using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IBeverageTypeRepository
{
    Task<BeverageType?> GetBeverageTypeByName(string name);
    Task<BeverageType?> GetBeverageTypeById(long id);
    Task<BeverageType> AddBeverageType(BeverageType beverageType);
    
    Task UpdateBeverageType(BeverageType beverageType);
    Task DeleteBeverageType(long id);
}