using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface ICafeService
{
    Task AddMenuItemAsync(MenuItem menuItem);
}