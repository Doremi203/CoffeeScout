using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface ICafeService
{
    Task AddMenuItemAsync(string adminId, MenuItem menuItem);
    Task AssignNewCafeAdminAsync(string adminId, long cafeId);
}