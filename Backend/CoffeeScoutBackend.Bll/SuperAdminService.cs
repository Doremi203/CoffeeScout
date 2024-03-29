using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll;

public class SuperAdminService(
    IMenuItemRepository menuItemRepository,
    IRoleRegistrationService roleRegistrationService
) : ISuperAdminService
{
    public async Task AddBeverageTypeAsync(BeverageType beverageType)
    {
        await menuItemRepository.AddBeverageTypeAsync(beverageType);
    }

    public async Task AddCafeAdminAsync(string requestEmail, string requestPassword)
    {
        var admin = new AppUser
        {
            UserName = requestEmail,
            Email = requestEmail,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        await roleRegistrationService.RegisterUserAsync(
            admin, requestPassword, Roles.CafeAdmin);
    }
}