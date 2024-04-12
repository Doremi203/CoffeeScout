using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class CafeService(
    ICafeRepository cafeRepository,
    ICoffeeChainService coffeeChainService
) : ICafeService
{
    public async Task<Cafe> GetById(long id)
    {
        return await cafeRepository.GetById(id)
               ?? throw new CafeNotFoundException(
                   $"Cafe with id {id} not found",
                   id);
    }

    public async Task<Cafe> GetByAdminId(string cafeAdminId)
    {
        return await cafeRepository.GetByAdminId(cafeAdminId)
               ?? throw new CafeNotFoundException(
                   $"Cafe for admin with id: {cafeAdminId} not found");
    }

    public async Task AssignNewCafeAdmin(string cafeAdminId, long cafeId)
    {
        var cafe = await GetById(cafeId);
        await cafeRepository.AddCafeAdmin(new CafeAdmin
        {
            Id = cafeAdminId,
            Cafe = cafe
        });
    }

    public async Task<IReadOnlyCollection<Cafe>> GetCafesInArea(Location location, double radius)
    {
        return await cafeRepository.GetCafesInArea(location, radius);
    }

    public async Task<Cafe> AddCafe(Cafe cafe)
    {
        var coffeeChain = await coffeeChainService.GetById(cafe.CoffeeChain.Id);
        
        var newCafe = cafe with
        {
            CoffeeChain = coffeeChain
        };
        
        return await cafeRepository.Add(newCafe);
    }

    public async Task UpdateCafe(string cafeAdminId, Cafe cafe)
    {
        var existingCafe = await GetByAdminId(cafeAdminId);
        var modifiedCafe = existingCafe with
        {
            Name = cafe.Name,
            Location = cafe.Location
        };

        await cafeRepository.Update(modifiedCafe);
    }
    
    public async Task DeleteCafe(long id)
    {
        var cafe = await GetById(id);
        
        await cafeRepository.Delete(cafe.Id);
    }
}