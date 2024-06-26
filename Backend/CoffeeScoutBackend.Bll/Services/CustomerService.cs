using System.Transactions;
using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.Domain.ServiceModels;
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

    public async Task AddFavoredMenuItem(string userId, long menuItemId)
    {
        var customer = await GetByUserId(userId);
        var menuItem = await menuItemService.GetById(menuItemId);

        if (customer.FavoriteMenuItems.Any(mi => mi.Id == menuItemId))
            throw new MenuItemAlreadyFavoredException(
                $"Menu item with id:{menuItemId} is already favored by customer with id:{userId}",
                menuItemId);

        await customerRepository.AddFavoredMenuItem(customer, menuItem);
    }

    public async Task<IReadOnlyCollection<MenuItem>> GetFavoredMenuItems(string userId)
    {
        var customer = await GetByUserId(userId);

        return customer.FavoriteMenuItems.ToList();
    }

    public async Task RemoveFavoredMenuItem(string userId, long menuItemId)
    {
        var customer = await GetByUserId(userId);
        var menuItem = await menuItemService.GetById(menuItemId);

        if (customer.FavoriteMenuItems.All(mi => mi.Id != menuItemId))
            throw new FavoredMenuItemNotFoundException(
                $"Menu item with id:{menuItemId} is not favored by customer with id:{userId}",
                menuItemId);

        await customerRepository.RemoveFavoredMenuItem(customer, menuItem);
    }

    public async Task<IReadOnlyCollection<BeverageType>> GetFavoredBeverageTypes(string userId)
    {
        var customer = await GetByUserId(userId);

        var favoredBeverageTypes =
            customer.FavoriteMenuItems
                .Select(mi => mi.BeverageType)
                .Distinct();

        return favoredBeverageTypes.ToList();
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