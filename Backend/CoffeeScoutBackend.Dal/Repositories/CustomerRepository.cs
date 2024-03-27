using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Mapster;

namespace CoffeeScoutBackend.Dal.Repositories;

public class CustomerRepository(
    AppDbContext dbContext
) : ICustomerRepository
{
    public async Task<Customer?> GetByIdAsync(string userId)
    {
        var customer = await dbContext.Customers.FindAsync(userId);
        return customer.Adapt<Customer>();
    }

    public async Task AddAsync(Customer customer)
    {
        await dbContext.Customers.AddAsync(customer.Adapt<CustomerEntity>());
        await dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(string userId, Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string userId)
    {
        throw new NotImplementedException();
    }
}