using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task RegisterCustomer(CustomerRegistrationData customerRegistrationData);
    Task<Customer> GetByUserId(string userId);
    Task AddFavoredMenuItem(string currentUserId, long menuItemId);
    Task<IReadOnlyCollection<BeverageType>> GetFavoredBeverageTypes(string userId);
}