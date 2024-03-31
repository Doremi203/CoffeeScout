using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ICafeService
{
    Task<Cafe> GetByIdAsync(long id);
    Task<Cafe> GetByAdminIdAsync(string adminId);
    Task AddMenuItemAsync(string adminId, MenuItem menuItem);
    Task AssignNewCafeAdminAsync(string adminId, long cafeId);
    Task<IReadOnlyCollection<Cafe>> GetCafesInAreaAsync(Location location, double radius);
}