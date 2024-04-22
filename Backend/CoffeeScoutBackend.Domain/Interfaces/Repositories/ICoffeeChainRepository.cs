using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface ICoffeeChainRepository
{
    Task<IReadOnlyCollection<CoffeeChain>> GetPage(int pageSize, int pageNumber);
    Task<CoffeeChain?> GetById(long id);
    Task<CoffeeChain> Add(CoffeeChain coffeeChain);
    Task Update(long id, CoffeeChain coffeeChain);
    Task Delete(long id);
}