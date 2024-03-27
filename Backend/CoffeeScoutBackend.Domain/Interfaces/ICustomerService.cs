using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface ICustomerService
{
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> GetByUserIdAsync(string userId);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> UpdateAsync(string userId, Customer customer);
    Task DeleteAsync(long userId);
}