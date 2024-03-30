using System.Transactions;
using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll;

public class CustomerService(
    ICustomerRepository customerRepository,
    IMenuItemService menuItemService,
    IRoleRegistrationService roleRegistrationService
) : ICustomerService
{
    public async Task RegisterCustomerAsync(CustomerRegistrationData customerRegistrationData)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var newUser = new AppUser
        {
            UserName = customerRegistrationData.Email, 
            Email = customerRegistrationData.Email,
        };

        var user = await roleRegistrationService
            .RegisterUserAsync(newUser, customerRegistrationData.Password, Roles.Customer);
        var customer = new Customer { UserId = user.Id, FirstName = customerRegistrationData.FirstName };

        await customerRepository.AddAsync(customer);
        scope.Complete();
    }

    public async Task<Customer> GetByUserIdAsync(string userId)
    {
        return await customerRepository.GetByIdAsync(userId)
               ?? throw new CustomerNotFoundException($"Customer with id:{userId} not found", userId);
    }

    public async Task AddFavoredMenuItemAsync(string currentUserId, long menuItemId)
    {
        var customer = await customerRepository.GetByIdAsync(currentUserId)
                       ?? throw new CustomerNotFoundException(
                           $"Customer with id:{currentUserId} not found",
                           currentUserId);
        var menuItem = await menuItemService.GetByIdAsync(menuItemId);
        
        if (customer.FavoriteMenuItems.Contains(menuItem))
            throw new MenuItemAlreadyFavoredException(
                $"Menu item with id:{menuItemId} is already favored by customer with id:{currentUserId}",
                menuItemId,
                currentUserId);
        await customerRepository.AddFavoredMenuItemAsync(customer, menuItem);
    }

    public async Task<IEnumerable<BeverageType>> GetFavoredBeverageTypesAsync(string userId)
    {
        var customer = await customerRepository.GetByIdAsync(userId);
        if (customer is null)
            throw new CustomerNotFoundException($"Customer with id:{userId} not found", userId);

        var favoredBeverageTypes =
            customer.FavoriteMenuItems
                .Select(mi => mi.BeverageType)
                .Distinct();

        return favoredBeverageTypes;
    }
}