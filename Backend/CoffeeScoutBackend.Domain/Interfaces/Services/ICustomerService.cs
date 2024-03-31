using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task RegisterCustomerAsync(CustomerRegistrationData customerRegistrationData);
    Task<Customer> GetByUserIdAsync(string userId);
    Task AddFavoredMenuItemAsync(string currentUserId, long menuItemId);
    Task<IReadOnlyCollection<BeverageType>> GetFavoredBeverageTypesAsync(string userId);
}