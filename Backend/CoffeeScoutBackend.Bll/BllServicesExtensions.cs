using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeScoutBackend.Bll;

public static class BllServicesExtensions
{
    public static IServiceCollection AddBllServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<ICafeService, CafeService>()
            .AddScoped<ISuperAdminService, SuperAdminService>()
            .AddScoped<IRoleRegistrationService, RoleRegistrationService>();

        return services;
    }
}