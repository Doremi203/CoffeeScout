using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class BeverageTypeRepository(
    AppDbContext dbContext
) : IBeverageTypeRepository
{
    public async Task<BeverageType?> GetBeverageTypeByNameAsync(string name)
    {
        var beverageType = await dbContext.BeverageTypes
            .FirstOrDefaultAsync(bt => bt.Name == name);
        return beverageType?.Adapt<BeverageType>();
    }

    public async Task<BeverageType?> GetBeverageTypeByIdAsync(long id)
    {
        var beverageType = await dbContext.BeverageTypes
            .FirstOrDefaultAsync(bt => bt.Id == id);
        return beverageType?.Adapt<BeverageType>();
    }

    public async Task AddBeverageTypeAsync(BeverageType beverageType)
    {
        var entity = beverageType.Adapt<BeverageTypeEntity>();
        await dbContext.BeverageTypes.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateBeverageTypeAsync(BeverageType beverageType)
    {
        var entity = await dbContext.BeverageTypes
            .FirstAsync(bt => bt.Id == beverageType.Id);
        entity.Name = beverageType.Name;
        dbContext.BeverageTypes.Update(entity);
        await dbContext.SaveChangesAsync();
    }
}