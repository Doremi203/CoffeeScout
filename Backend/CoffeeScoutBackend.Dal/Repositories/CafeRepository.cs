using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Dal.Infrastructure;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class CafeRepository(
    AppDbContext dbContext,
    ILocationProvider locationProvider
) : ICafeRepository
{
    public async Task<Cafe?> GetById(long id)
    {
        var cafeEntity = await GetCafes()
            .FirstOrDefaultAsync(ca => ca.Id == id);

        return cafeEntity?.Adapt<Cafe>();
    }

    public async Task<Cafe?> GetByAdminId(string adminId)
    {
        var cafe = await GetCafes()
            .FirstOrDefaultAsync(ca => ca.Admins.Any(ad => ad.Id == adminId));

        return cafe.Adapt<Cafe>();
    }

    public async Task AddCafeAdmin(CafeAdmin admin)
    {
        var adminEntity = admin.Adapt<CafeAdminEntity>();
        adminEntity.Cafe = await dbContext.Cafes
            .FirstAsync(ca => ca.Id == admin.Cafe.Id);

        await dbContext.CafeAdmins.AddAsync(adminEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Cafe>> GetCafesInArea(Location location, double radius)
    {
        var area = locationProvider.CreateArea(location, radius);

        var cafes = await GetCafes()
            .Where(c => c.Location.Within(area))
            .ToListAsync();

        return cafes.Adapt<IReadOnlyCollection<Cafe>>();
    }

    public async Task<Cafe> Add(Cafe cafe)
    {
        var cafeEntity = cafe.Adapt<CafeEntity>();
        cafeEntity.CoffeeChain = await dbContext.CoffeeChains
            .FirstAsync(cc => cc.Id == cafe.CoffeeChain.Id);

        await dbContext.Cafes.AddAsync(cafeEntity);
        await dbContext.SaveChangesAsync();

        return cafeEntity.Adapt<Cafe>();
    }

    public async Task Update(Cafe cafe)
    {
        var cafeData = cafe.Adapt<CafeEntity>();
        var cafeEntity = dbContext.Cafes
            .Include(cafeEntity => cafeEntity.WorkingHours)
            .First(ca => ca.Id == cafe.Id);

        cafeEntity.Name = cafeData.Name;
        cafeEntity.Location = cafeData.Location;
        cafeEntity.Address = cafeData.Address;

        foreach (var workingHours in cafeData.WorkingHours)
        {
            var existingWorkingHours = cafeEntity.WorkingHours
                .First(wh => wh.DayOfWeek == workingHours.DayOfWeek);

            existingWorkingHours.OpeningTime = workingHours.OpeningTime;
            existingWorkingHours.ClosingTime = workingHours.ClosingTime;
        }

        dbContext.Cafes.Update(cafeEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var cafe = await dbContext.Cafes
            .FirstAsync(ca => ca.Id == id);

        dbContext.Cafes.Remove(cafe);
        await dbContext.SaveChangesAsync();
    }

    private IQueryable<CafeEntity> GetCafes()
    {
        return dbContext.Cafes
            .Include(ca => ca.Admins)
            .Include(ca => ca.CoffeeChain)
            .Include(ca => ca.WorkingHours)
            .Include(ca => ca.MenuItems)
            .ThenInclude(mi => mi.BeverageType);
    }
}