using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface ICustomerService
{
    Task RegisterCustomerAsync(CustomerRegistrationData customerRegistrationData);
    Task<Customer> GetByUserIdAsync(string userId);
    Task AddFavoredMenuItemAsync(string currentUserId, long menuItemId);
    Task<IEnumerable<BeverageType>> GetFavoredBeverageTypesAsync(string userId);
}