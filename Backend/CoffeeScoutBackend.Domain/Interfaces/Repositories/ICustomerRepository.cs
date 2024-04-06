using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetById(string userId);
    Task Add(Customer customer);
    public Task AddFavoredMenuItem(Customer customer, MenuItem menuItem);
}