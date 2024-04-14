using CoffeeScoutBackend.Bll.Infrastructure;
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
            .AddScoped<IPaymentService, PaymentService>()
            .AddScoped<IReviewService, ReviewService>()
            .AddScoped<IBeverageTypeService, BeverageTypeService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<ICoffeeChainService, CoffeeChainService>()
            .AddScoped<ICafeService, CafeService>()
            .AddScoped<IMenuItemService, MenuItemService>()
            .AddScoped<ISuperAdminService, SuperAdminService>();
        
        services
            .AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}