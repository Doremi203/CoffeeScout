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

    public async Task<BeverageType> AddBeverageType(BeverageType beverageType)
    {
        var beverageTypeEntity = beverageType.Adapt<BeverageTypeEntity>();
        
        await dbContext.BeverageTypes.AddAsync(beverageTypeEntity);
        await dbContext.SaveChangesAsync();
        
        return beverageTypeEntity.Adapt<BeverageType>();
    }

    public async Task UpdateBeverageType(BeverageType beverageType)
    {
        var beverageTypeEntity = await dbContext.BeverageTypes
            .FirstAsync(bt => bt.Id == beverageType.Id);
        beverageTypeEntity.Name = beverageType.Name;
        
        dbContext.BeverageTypes.Update(beverageTypeEntity);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteBeverageType(long id)
    {
        var beverageType = await dbContext.BeverageTypes
            .FirstAsync(bt => bt.Id == id);
        
        dbContext.BeverageTypes.Remove(beverageType);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<BeverageType>> GetBeverageTypes(int pageSize, int pageNumber)
    {
        var beverageTypes = await dbContext.BeverageTypes
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        
        return beverageTypes.Adapt<IReadOnlyCollection<BeverageType>>();
    }
}