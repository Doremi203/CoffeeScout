using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Bll.Services;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeScoutBackend.Bll;

public static class BllServicesExtensions
{
    public static IServiceCollection AddBllServices(this IServiceCollection services)
    {
        services
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<ICafeService, CafeService>()
            .AddScoped<IMenuItemService, MenuItemService>()
            .AddScoped<ISuperAdminService, SuperAdminService>()
            .AddScoped<IRoleRegistrationService, RoleRegistrationService>();

        return services;
    }
}