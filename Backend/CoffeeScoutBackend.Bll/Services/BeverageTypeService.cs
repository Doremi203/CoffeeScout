using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class BeverageTypeService(
    IBeverageTypeRepository beverageTypeRepository
) : IBeverageTypeService
{ 
    public async Task<BeverageType> GetById(long id)
    {
        return await beverageTypeRepository
                   .GetById(id)
               ?? throw new BeverageTypeNotFoundException(
                   $"Beverage type with id {id} not found",
                   id);
    }

    public async Task<BeverageType> Add(BeverageType beverageType)
    {
        return await beverageTypeRepository.Add(beverageType);
    }

    public async Task Update(long id, BeverageType beverageType)
    {
        var beverage = await GetById(id);
        var updatedBeverage = beverage with
        {
            Name = beverageType.Name,
            Description = beverageType.Description
        };
        
        await beverageTypeRepository.Update(updatedBeverage);
    }

    public async Task Delete(long id)
    {
        var beverage = await GetById(id);
        
        await beverageTypeRepository.Delete(beverage.Id);
    }

    public async Task<IReadOnlyCollection<BeverageType>> GetPage(int pageSize, int pageNumber)
    {
        return await beverageTypeRepository.GetPage(pageSize, pageNumber);
    }
}