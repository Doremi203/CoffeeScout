using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IBeverageTypeService
{
    Task<BeverageType> GetBeverageTypeByNameAsync(string name);
    Task<BeverageType> GetBeverageTypeById(long id);
    Task<BeverageType> AddBeverageType(BeverageType beverageType);
    Task UpdateBeverageTypeName(long id, string name);
    Task DeleteBeverageType(long id);
    Task<IReadOnlyCollection<BeverageType>> GetBeverageTypes(int pageSize, int pageNumber);
}