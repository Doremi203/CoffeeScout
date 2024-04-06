using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class BeverageTypeService(
    IBeverageTypeRepository beverageTypeRepository
) : IBeverageTypeService
{
    public async Task<BeverageType> GetBeverageTypeByNameAsync(string name)
    {
        return await beverageTypeRepository
                   .GetBeverageTypeByName(name)
               ?? throw new BeverageTypeNotFoundException(
                   $"Beverage type with name {name} not found",
                   name);
    }

    public async Task<BeverageType> GetBeverageTypeById(long id)
    {
        return await beverageTypeRepository
                   .GetBeverageTypeById(id)
               ?? throw new BeverageTypeNotFoundException(
                   $"Beverage type with id {id} not found",
                   id);
    }

    public Task AddBeverageType(BeverageType beverageType)
    {
        return beverageTypeRepository.AddBeverageType(beverageType);
    }

    public async Task UpdateBeverageTypeName(long id, string name)
    {
        var beverage = await GetBeverageTypeById(id);
        
        await beverageTypeRepository.UpdateBeverageType(
            beverage with { Name = name });
    }

    public async Task DeleteBeverageType(long id)
    {
        var beverage = await GetBeverageTypeById(id);
        
        await beverageTypeRepository.DeleteBeverageType(beverage.Id);
    }
}