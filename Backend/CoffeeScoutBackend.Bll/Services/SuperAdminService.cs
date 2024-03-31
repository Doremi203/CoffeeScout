using System.Transactions;
using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class SuperAdminService(
    IRoleRegistrationService roleRegistrationService,
    ICafeService cafeService
) : ISuperAdminService
{
    public async Task AddCafeAdminAsync(
        CafeAdminRegistrationData registrationData
    )
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var admin = new AppUser
        {
            UserName = registrationData.Email,
            Email = registrationData.Email,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        await roleRegistrationService.RegisterUserAsync(
            admin, registrationData.Password, Roles.CafeAdmin);

        await cafeService.AssignNewCafeAdminAsync(admin.Id, registrationData.CafeId);

        transaction.Complete();
    }
}