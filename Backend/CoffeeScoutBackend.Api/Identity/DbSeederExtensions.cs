using CoffeeScoutBackend.Api.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CoffeeScoutBackend.Api.Identity;

public static class DbSeederExtensions
{
    public static async Task SeedRolesAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in Enum.GetNames<Roles>())
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
    
    public static async Task SeedSuperAdminAsync(
        this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
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
            }
        }
    }
}