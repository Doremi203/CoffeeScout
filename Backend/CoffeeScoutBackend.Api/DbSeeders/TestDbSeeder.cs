using System.Transactions;
using CoffeeScoutBackend.Api.Config;
using CoffeeScoutBackend.Dal;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Dal.Infrastructure;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoffeeScoutBackend.Api.DbSeeders;

public class TestDbSeeder(
    IServiceProvider serviceProvider
) : IDbSeeder
{
    public async Task SeedDbAsync()
    {
        await SeedRolesAsync();
        await SeedSuperAdminAsync();
        await SeedBeverageTypesAsync();
        await SeedCafeAsync();
        await SeedMenuItemsAsync();
        await SeedCafeAdminAsync();
    }

    private async Task SeedCafeAdminAsync()
    {
        using var scope = serviceProvider.CreateScope();
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
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
                var cafe = await dbContext.Cafes.FirstAsync();
                await dbContext.CafeAdmins.AddAsync(new CafeAdminEntity
                {
                    User = admin,
                    Id = admin.Id,
                    Cafe = cafe,
                    CafeId = cafe.Id
                });
                await dbContext.SaveChangesAsync();
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
                new() { Name = "Espresso" }
            };
            await dbContext.BeverageTypes.AddRangeAsync(beverageTypes);
            await dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedCafeAsync()
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var locationProvider = scope.ServiceProvider.GetRequiredService<ILocationProvider>();
        if (!dbContext.Cafes.Any())
        {
            var cafes = new List<CafeEntity>
            {
                new()
                {
                    Name = "Coffee Crew",
                    Location = locationProvider.CreatePoint(
                        55.698964, 37.499202)
                },
                new()
                {
                    Name = "Stars Coffee",
                    Location = locationProvider.CreatePoint(
                        55.697503, 37.500088)
                }
            };
            await dbContext.Cafes.AddRangeAsync(cafes);
            await dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedMenuItemsAsync()
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!dbContext.MenuItems.Any())
        {
            var coffeeCrew = await dbContext.Cafes.FirstAsync(c => c.Name == "Coffee Crew");
            var starsCoffee = await dbContext.Cafes.FirstAsync(c => c.Name == "Stars Coffee");
            var cappuccino = await dbContext.BeverageTypes.FirstAsync(b => b.Name == "Cappuccino");
            var menuItems = new List<MenuItemEntity>
            {
                new()
                {
                    Name = "Капучино малый",
                    Cafe = coffeeCrew,
                    CafeId = coffeeCrew.Id,
                    BeverageType = cappuccino,
                    BeverageTypeId = cappuccino.Id,
                    Price = 240
                },
                new()
                {
                    Name = "Капучино большой",
                    Cafe = starsCoffee,
                    CafeId = starsCoffee.Id,
                    BeverageType = cappuccino,
                    BeverageTypeId = cappuccino.Id,
                    Price = 325
                }
            };
            
            await dbContext.MenuItems.AddRangeAsync(menuItems);
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