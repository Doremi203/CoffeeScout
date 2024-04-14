using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task RegisterCustomer(CustomerRegistrationData customerRegistrationData);
    Task<Customer> GetByUserId(string userId);
    Task AddFavoredMenuItem(string userId, long menuItemId);
    Task<IReadOnlyCollection<MenuItem>> GetFavoredMenuItems(string userId);
    Task RemoveFavoredMenuItem(string userId, long menuItemId);
    Task<IReadOnlyCollection<BeverageType>> GetFavoredBeverageTypes(string userId);
    Task<CustomerInfo> GetInfo(string userId);
    Task UpdateInfo(string userId, CustomerInfo info);
}