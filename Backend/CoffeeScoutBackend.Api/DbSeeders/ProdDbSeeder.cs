using System.Transactions;
using CoffeeScoutBackend.Api.Config;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CoffeeScoutBackend.Api.DbSeeders;

public class ProdDbSeeder(
    IServiceProvider serviceProvider
) : IDbSeeder
{
    public async Task SeedDbAsync()
    {
        await SeedRolesAsync();
        await SeedSuperAdminAsync();
    }

    private async Task SeedRolesAsync()
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in Enum.GetNames<Roles>())
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
    }

    private async Task SeedSuperAdminAsync()
    {
        using var scope = serviceProvider.CreateScope();
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var adminSettings = scope.ServiceProvider.GetRequiredService<IOptions<AdminSettings>>();
        var adminEmail = adminSettings.Value.Email;
        var adminPassword = adminSettings.Value.Password;

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var result = await userManager.CreateAsync(admin, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, Roles.SuperAdmin.ToString());
                transaction.Complete();
            }
        }
    }
}