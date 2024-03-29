using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface ICafeRepository
{ 
    Task<Cafe> GetCafeByAdminIdAsync(string adminId);
}