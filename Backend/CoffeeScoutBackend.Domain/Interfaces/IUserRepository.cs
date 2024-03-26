using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long userId);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(long userId, User user);
    Task DeleteAsync(long userId);
}