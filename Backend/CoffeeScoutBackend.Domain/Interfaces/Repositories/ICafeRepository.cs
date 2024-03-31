using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface ICafeRepository
{
    Task<Cafe?> GetByIdAsync(long id);
    Task<Cafe?> GetByAdminIdAsync(string adminId);
    Task CreateCafeAdminAsync(CafeAdmin admin);
}