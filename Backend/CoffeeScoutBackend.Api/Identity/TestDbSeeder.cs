using System.Transactions;
using CoffeeScoutBackend.Api.Config;
using CoffeeScoutBackend.Dal;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CoffeeScoutBackend.Api.Identity;

public class TestDbSeeder(
    IServiceProvider serviceProvider
) : IDbSeeder
{
    public async Task SeedDbAsync()
    {
        await SeedRolesAsync();
        await SeedSuperAdminAsync();
        await SeedBeverageTypesAsync();
        await SeedMenuItemsAsync();
        await SeedCafeAdminAsync();
    }

    private async Task SeedCafeAdminAsync()
    {
        using var scope = serviceProvider.CreateScope();
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var adminEmail = "cafeAdmin@gmail.com";
        var adminPassword = "CafeAdminPro@1";

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
                await userManager.AddToRoleAsync(admin, Roles.CafeAdmin.ToString());
                transaction.Complete();
            }
        }
    }

    private async Task SeedBeverageTypesAsync()
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!dbContext.BeverageTypes.Any())
        {
            var beverageTypes = new List<BeverageTypeEntity>
            {
                new() { Name = "Cappuccino" },
                new() { Name = "Espresso" },
            };
            await dbContext.BeverageTypes.AddRangeAsync(beverageTypes);
            await dbContext.SaveChangesAsync();
        }
    }
    
    private async Task SeedMenuItemsAsync()
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!dbContext.MenuItems.Any())
        {
            foreach (var beverageType in dbContext.BeverageTypes)
            {
                await dbContext.MenuItems.AddAsync(new MenuItemEntity
                {
                    Name = $"{beverageType.Name} 0.2l",
                    Price = 2.5m,
                    BeverageTypeEntity = beverageType,
                    BeverageTypeEntityId = beverageType.Id
                });
            }
            await dbContext.SaveChangesAsync();
        }
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