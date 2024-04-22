using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ICafeService
{
    Task<Cafe> GetById(long id);
    Task<Cafe> GetByAdminId(string cafeAdminId);
    Task AssignNewCafeAdmin(string cafeAdminId, long cafeId);
    Task<IReadOnlyCollection<Cafe>> GetCafesInArea(Location location, double radius);
    Task<Cafe> AddCafe(Cafe cafe);
    Task UpdateCafe(string cafeAdminId, Cafe cafe);
    Task DeleteCafe(long id);
}