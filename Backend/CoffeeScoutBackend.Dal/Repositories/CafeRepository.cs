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
    public async Task<Cafe?> GetByIdAsync(long id)
    {
        var cafeEntity = await dbContext.Cafes
            .Include(ca => ca.Admins)
            .FirstOrDefaultAsync(ca => ca.Id == id);

        return cafeEntity?.Adapt<Cafe>();
    }

    public async Task<Cafe?> GetByAdminIdAsync(string adminId)
    {
        var admin = await dbContext.CafeAdmins
            .Include(ca => ca.Cafe)
            .FirstOrDefaultAsync(ca => ca.UserId == adminId);

        return admin?.Cafe.Adapt<Cafe>();
    }

    public async Task CreateCafeAdminAsync(CafeAdmin admin)
    {
        var adminEntity = admin.Adapt<CafeAdminEntity>();
        adminEntity.Cafe = await dbContext.Cafes
            .FirstAsync(ca => ca.Id == admin.Cafe.Id);

        dbContext.CafeAdmins.Add(adminEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Cafe>> GetCafesInAreaAsync(Location location, double radius)
    {
        var area = locationProvider.CreateArea(location, radius);
        
        var cafes = await dbContext.Cafes
            .Include(c => c.MenuItems)
            .Where(c => c.Location.Within(area))
            .ToListAsync();

        return cafes.Adapt<IReadOnlyCollection<Cafe>>();
    }
}