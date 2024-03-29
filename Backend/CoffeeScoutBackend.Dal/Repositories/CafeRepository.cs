using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Repositories;

public class CafeRepository(
    AppDbContext dbContext
) : ICafeRepository
{
    public async Task<Cafe> GetCafeByAdminIdAsync(string adminId)
    {
        var admin = await dbContext.CafeAdmins
            .Include(ca => ca.Cafe)
            .FirstAsync(ca => ca.UserId == adminId);
        return admin.Cafe.Adapt<Cafe>();
    }
}