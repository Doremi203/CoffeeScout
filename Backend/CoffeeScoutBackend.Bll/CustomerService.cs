using System.Transactions;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CoffeeScoutBackend.Bll;

public class CustomerService(
    ICustomerRepository customerRepository,
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
        
        throw new RegistrationException("Customer registration failed", errors);
    }

    private static void AddRegistrationErrors(IdentityResult result, Dictionary<string, string[]> errors)
    {
        foreach (var error in result.Errors.Where(SkipExtraErrors))
        {
            errors[error.Code] = [error.Description];
        }
    }

    private static bool SkipExtraErrors(IdentityError arg)
    {
        return arg.Code != "DuplicateUserName";
    }

    public Task<Customer> GetByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> UpdateAsync(string userId, Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long userId)
    {
        throw new NotImplementedException();
    }
}