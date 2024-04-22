using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ICoffeeChainService
{
    Task<IReadOnlyCollection<CoffeeChain>> GetPage(int pageSize, int pageNumber);
    Task<CoffeeChain> GetById(long id);
    Task<CoffeeChain> Add(CoffeeChain coffeeChain);
    Task Update(long id, CoffeeChain coffeeChain);
    Task Delete(long id);
}