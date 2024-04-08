using CoffeeScoutBackend.Api.Identity.Services;
using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal;
using CoffeeScoutBackend.Dal.Entities;
using Microsoft.AspNetCore.Identity;

namespace CoffeeScoutBackend.Api.Identity;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services)
    {
        services.AddIdentityCore<AppUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        services.AddIdentityApiEndpoints<AppUser>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });

        services
            .AddScoped<IRoleRegistrationService, RoleRegistrationService>()
            .AddScoped<IEmailConfirmationService, EmailConfirmationService>();

        return services;
    }
}