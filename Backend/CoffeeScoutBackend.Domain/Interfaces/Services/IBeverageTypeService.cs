using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IBeverageTypeService
{
    Task<BeverageType> GetById(long id);
    Task<BeverageType> Add(BeverageType beverageType);
    Task Update(long id, BeverageType beverageType);
    Task Delete(long id);
    Task<IReadOnlyCollection<BeverageType>> GetPage(int pageSize, int pageNumber);
}