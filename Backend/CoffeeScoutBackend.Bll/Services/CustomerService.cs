using System.Transactions;
using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;

namespace CoffeeScoutBackend.Bll.Services;

public class CustomerService(
    ICustomerRepository customerRepository,
    IMenuItemService menuItemService,
    IRoleRegistrationService roleRegistrationService
) : ICustomerService
{
    public async Task RegisterCustomer(CustomerRegistrationData customerRegistrationData)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var newUser = new AppUser
        {
            UserName = customerRegistrationData.Email,
            Email = customerRegistrationData.Email
        };

        var user = await roleRegistrationService
            .RegisterUser(newUser, customerRegistrationData.Password, Roles.Customer);
        var customer = new Customer { Id = user.Id, FirstName = customerRegistrationData.FirstName };

        await customerRepository.Add(customer);
        scope.Complete();
    }

    public async Task<Customer> GetByUserId(string userId)
    {
        return await customerRepository.GetById(userId)
               ?? throw new CustomerNotFoundException($"Customer with id:{userId} not found", userId);
    }

    public async Task AddFavoredMenuItem(string currentUserId, long menuItemId)
    {
        var customer = await customerRepository.GetById(currentUserId)
                       ?? throw new CustomerNotFoundException(
                           $"Customer with id:{currentUserId} not found",
                           currentUserId);
        var menuItem = await menuItemService.GetById(menuItemId);

        if (customer.FavoriteMenuItems.Contains(menuItem))
            throw new MenuItemAlreadyFavoredException(
                $"Menu item with id:{menuItemId} is already favored by customer with id:{currentUserId}",
                menuItemId,
                currentUserId);
        await customerRepository.AddFavoredMenuItem(customer, menuItem);
    }

    public async Task<IReadOnlyCollection<BeverageType>> GetFavoredBeverageTypes(string userId)
    {
        var customer = await GetByUserId(userId);

        var favoredBeverageTypes =
            customer.FavoriteMenuItems
                .Select(mi => mi.BeverageType)
                .Distinct();

        return favoredBeverageTypes.ToList()!;
    }
    
    public async Task<CustomerInfo> GetInfo(string userId)
    {
        var customer = await GetByUserId(userId);

        return customer.Adapt<CustomerInfo>();
    }

    public async Task UpdateInfo(string userId, CustomerInfo info)
    {
        var customer = await GetByUserId(userId);

        var updatedCustomer = customer with { FirstName = info.FirstName };

        await customerRepository.Update(updatedCustomer);
    }
}