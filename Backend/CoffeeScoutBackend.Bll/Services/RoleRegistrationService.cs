using System.Transactions;
using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CoffeeScoutBackend.Bll.Services;

public class RoleRegistrationService(
    UserManager<AppUser> userManager
) : IRoleRegistrationService
{
    public async Task<AppUser> RegisterUserAsync(AppUser user, string password, Roles role)
    {
        var errors = new Dictionary<string, string[]>();
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            var roleResult = await userManager.AddToRoleAsync(user, role.ToString());
            if (roleResult.Succeeded)
            {
                scope.Complete();
                return user;
            }

            AddRegistrationErrors(roleResult, errors);
        }

        AddRegistrationErrors(result, errors);

        throw new RegistrationException("Registration failed", errors);
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