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
                dbContext.CafeAdmins.Add(new CafeAdminEntity
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
                new()
                {
                    Name = "Капучино",
                    Description = "Кофе с молоком и молочной пенкой"
                },
                new()
                {
                    Name = "Эспрессо",
                    Description = "Крепкий кофе без добавок"
                }
            };
            dbContext.BeverageTypes.AddRange(beverageTypes);
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
                        55.698964, 37.499202),
                    Address = "ул. Ленинградская, 10",
                    CoffeeChain = new CoffeeChainEntity { Name = "Coffee Crew" },
                    WorkingHours = new List<WorkingHoursEntity>()
                    {
                        new()
                        {
                            DayOfWeek = DayOfWeek.Monday,
                            OpeningTime = new TimeOnly(8, 0),
                            ClosingTime = new TimeOnly(20, 0)
                        },
                        new()
                        {
                            DayOfWeek = DayOfWeek.Tuesday,
                            OpeningTime = new TimeOnly(8, 0),
                            ClosingTime = new TimeOnly(20, 0)
                        },
                    }
                },
                new()
                {
                    Name = "Stars Coffee",
                    Location = locationProvider.CreatePoint(
                        55.697503, 37.500088),
                    Address = "ул. Ленинградская, 12",
                    CoffeeChain = new CoffeeChainEntity { Name = "Stars Coffee" },
                    WorkingHours = new List<WorkingHoursEntity>()
                    {
                        new()
                        {
                            DayOfWeek = DayOfWeek.Monday,
                            OpeningTime = new TimeOnly(8, 0),
                            ClosingTime = new TimeOnly(20, 0)
                        },
                        new()
                        {
                            DayOfWeek = DayOfWeek.Tuesday,
                            OpeningTime = new TimeOnly(8, 0),
                            ClosingTime = new TimeOnly(20, 0)
                        },
                    }
                }
            };
            dbContext.Cafes.AddRange(cafes);
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
                    Price = 240,
                    SizeInMl = 180,
                    Reviews = new List<ReviewEntity>(),
                    CustomersFavoredBy = new List<CustomerEntity>()
                },
                new()
                {
                    Name = "Капучино большой",
                    Cafe = starsCoffee,
                    CafeId = starsCoffee.Id,
                    BeverageType = cappuccino,
                    BeverageTypeId = cappuccino.Id,
                    Price = 325,
                    SizeInMl = 300,
                    Reviews = new List<ReviewEntity>(),
                    CustomersFavoredBy = new List<CustomerEntity>()
                }
            };

            dbContext.MenuItems.AddRange(menuItems);
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