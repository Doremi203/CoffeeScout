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
                   .GetBeverageTypeByNameAsync(name)
               ?? throw new BeverageTypeNotFoundException(
                   $"Beverage type with name {name} not found",
                   name);
    }

    public async Task<BeverageType> GetBeverageTypeByIdAsync(long id)
    {
        return await beverageTypeRepository
                   .GetBeverageTypeByIdAsync(id)
               ?? throw new BeverageTypeNotFoundException(
                   $"Beverage type with id {id} not found",
                   id);
    }

    public Task AddBeverageTypeAsync(BeverageType beverageType)
    {
        return beverageTypeRepository.AddBeverageTypeAsync(beverageType);
    }

    public async Task UpdateBeverageTypeNameAsync(long id, string name)
    {
        var beverage = await GetBeverageTypeByIdAsync(id);
        
        await beverageTypeRepository.UpdateBeverageTypeAsync(
            beverage with { Name = name });
    }
}