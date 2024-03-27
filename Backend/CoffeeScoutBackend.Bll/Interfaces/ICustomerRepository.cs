using CoffeeScoutBackend.Dal.Entities;

namespace CoffeeScoutBackend.Bll.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerEntity?> GetByIdAsync(long customerId);
    Task AddAsync(CustomerEntity customer);
    Task UpdateAsync(long customerId, CustomerEntity customer);
    Task DeleteAsync(long customerId);
}