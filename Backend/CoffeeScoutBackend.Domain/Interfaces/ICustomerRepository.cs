using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(string userId);
    Task AddAsync(Customer customer);
    public Task AddFavoredMenuItemAsync(Customer customer, MenuItem menuItem);
    Task UpdateAsync(string userId, Customer customer);
    Task DeleteAsync(string userId);
}