using System.Transactions;
using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Services;

public class CafeAdminService(
    IRoleRegistrationService roleRegistrationService,
    ICafeService cafeService
) : ICafeAdminService
{
    public async Task AddCafeAdmin(
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

        await roleRegistrationService.RegisterUser(
            admin, registrationData.Password, Roles.CafeAdmin);

        await cafeService.AssignNewCafeAdmin(admin.Id, registrationData.CafeId);

        transaction.Complete();
    }
}