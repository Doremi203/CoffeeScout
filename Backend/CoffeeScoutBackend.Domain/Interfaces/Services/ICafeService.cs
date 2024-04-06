using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ICafeService
{
    Task<Cafe> GetById(long id);
    Task<Cafe> GetByAdminId(string adminId);
    Task AddMenuItem(string adminId, MenuItem menuItem);
    Task AssignNewCafeAdmin(string adminId, long cafeId);
    Task<IReadOnlyCollection<Cafe>> GetCafesInArea(Location location, double radius);
}