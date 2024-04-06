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
    public async Task<BeverageType?> GetBeverageTypeByName(string name)
    {
        var beverageType = await dbContext.BeverageTypes
            .FirstOrDefaultAsync(bt => bt.Name == name);
        
        return beverageType?.Adapt<BeverageType>();
    }

    public async Task<BeverageType?> GetBeverageTypeById(long id)
    {
        var beverageType = await dbContext.BeverageTypes
            .FirstOrDefaultAsync(bt => bt.Id == id);
        
        return beverageType?.Adapt<BeverageType>();
    }

    public async Task AddBeverageType(BeverageType beverageType)
    {
        var entity = beverageType.Adapt<BeverageTypeEntity>();
        
        await dbContext.BeverageTypes.AddAsync(entity);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateBeverageType(BeverageType beverageType)
    {
        var entity = await dbContext.BeverageTypes
            .FirstAsync(bt => bt.Id == beverageType.Id);
        entity.Name = beverageType.Name;
        
        dbContext.BeverageTypes.Update(entity);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteBeverageType(long id)
    {
        var entity = await dbContext.BeverageTypes
            .FirstAsync(bt => bt.Id == id);
        
        dbContext.BeverageTypes.Remove(entity);
        
        await dbContext.SaveChangesAsync();
    }
}