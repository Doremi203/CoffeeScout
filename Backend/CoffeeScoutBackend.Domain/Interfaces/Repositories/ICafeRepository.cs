using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface ICafeRepository
{
    Task<Cafe?> GetById(long id);
    Task<Cafe?> GetByAdminId(string adminId);
    Task CreateCafeAdmin(CafeAdmin admin);
    Task<IReadOnlyCollection<Cafe>> GetCafesInArea(Location location, double radius);
}