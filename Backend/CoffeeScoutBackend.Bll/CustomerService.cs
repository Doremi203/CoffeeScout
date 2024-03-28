using System.Transactions;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CoffeeScoutBackend.Bll;

public class CustomerService(
    ICustomerRepository customerRepository,
    IMenuItemRepository menuItemRepository,
    UserManager<AppUser> userManager
) : ICustomerService
{
    public async Task RegisterCustomerAsync(RegistrationData registrationData)
    {
        var errors = new Dictionary<string, string[]>();
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var firstName = registrationData.FirstName;
        var email = registrationData.Email;
        var password = registrationData.Password;
        var newUser = new AppUser { UserName = email, Email = email };
        var result = await userManager.CreateAsync(newUser, password);

        if (result.Succeeded)
        {
            var roleResult = await userManager.AddToRoleAsync(newUser, Roles.Customer.ToString());
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                throw new UserNotFoundException("User was not present, but should have been created");
            if (roleResult.Succeeded)
            {
                await customerRepository.AddAsync(new Customer
                {
                    UserId = user.Id,
                    FirstName = firstName
                });
                scope.Complete();
                return;
            }

            AddRegistrationErrors(roleResult, errors);
        }

        AddRegistrationErrors(result, errors);

        throw new CustomerRegistrationException("Customer registration failed", errors);
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
        var menuItem = await menuItemRepository.GetByIdAsync(menuItemId)
                       ?? throw new MenuItemNotFoundException(
                           $"Menu item with id:{menuItemId} not found",
                           menuItemId);

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

    private static void AddRegistrationErrors(IdentityResult result, Dictionary<string, string[]> errors)
    {
        foreach (var error in result.Errors.Where(SkipExtraErrors)) errors[error.Code] = [error.Description];
    }

    private static bool SkipExtraErrors(IdentityError arg)
    {
        return arg.Code != "DuplicateUserName";
    }
}