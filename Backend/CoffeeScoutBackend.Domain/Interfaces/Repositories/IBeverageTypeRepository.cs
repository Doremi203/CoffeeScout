using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface IBeverageTypeRepository
{
    Task<BeverageType?> GetBeverageTypeByName(string name);
    Task<BeverageType?> GetById(long id);
    Task<BeverageType> Add(BeverageType beverageType);
    
    Task Update(BeverageType beverageType);
    Task Delete(long id);
    Task<IReadOnlyCollection<BeverageType>> GetPage(int pageSize, int pageNumber);
}