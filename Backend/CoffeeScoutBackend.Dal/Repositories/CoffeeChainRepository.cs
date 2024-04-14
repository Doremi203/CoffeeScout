using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class CoffeeChainRepository(
    AppDbContext dbContext
) : ICoffeeChainRepository
{
    public async Task<CoffeeChain?> GetById(long id)
    {
        var coffeeChainEntity = await dbContext.CoffeeChains.FindAsync(id);

        return coffeeChainEntity?.Adapt<CoffeeChain>();
    }

    public async Task<CoffeeChain> Add(CoffeeChain coffeeChain)
    {
        var coffeeChainEntity = coffeeChain.Adapt<CoffeeChainEntity>();

        dbContext.CoffeeChains.Add(coffeeChainEntity);
        await dbContext.SaveChangesAsync();

        return coffeeChainEntity.Adapt<CoffeeChain>();
    }

    public async Task Update(long id, CoffeeChain coffeeChain)
    {
        var coffeeChainEntity = await dbContext.CoffeeChains
            .FirstAsync(c => c.Id == id);

        dbContext.CoffeeChains.Update(coffeeChainEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        await dbContext.CoffeeChains
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        
        await dbContext.SaveChangesAsync();
    }
}