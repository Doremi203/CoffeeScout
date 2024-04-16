using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class CoffeeChainService(
    ICoffeeChainRepository coffeeChainRepository
) : ICoffeeChainService
{
    public async Task<IReadOnlyCollection<CoffeeChain>> GetPage(int pageSize, int pageNumber)
    {
        return await coffeeChainRepository.GetPage(pageSize, pageNumber);
    }

    public async Task<CoffeeChain> GetById(long id)
    {
        return await coffeeChainRepository.GetById(id)
            ?? throw new CoffeeChainNotFoundException(
                $"Coffee chain with id {id} not found",
                id);
    }

    public async Task<CoffeeChain> Add(CoffeeChain coffeeChain)
    {
        return await coffeeChainRepository.Add(coffeeChain);
    }

    public async Task Update(long id, CoffeeChain coffeeChain)
    {
        var existingCoffeeChain = await GetById(id);
        
        await coffeeChainRepository.Update(existingCoffeeChain.Id, coffeeChain);
    }

    public async Task Delete(long id)
    {
        var existingCoffeeChain = await GetById(id);
        
        await coffeeChainRepository.Delete(existingCoffeeChain.Id);
    }
}