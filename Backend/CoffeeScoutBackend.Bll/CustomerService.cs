using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CoffeeScoutBackend.Bll;

public class CustomerService(
    ICustomerRepository customerRepository,
    UserManager<AppUser> userManager
) : ICustomerService
{
    public async Task<Customer> CreateAsync(Customer customer)
    {
        await userManager.FindByIdAsync(customer.UserId);
        throw new NotImplementedException();
    }

    public Task<Customer> GetByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> UpdateAsync(string userId, Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long userId)
    {
        throw new NotImplementedException();
    }
}