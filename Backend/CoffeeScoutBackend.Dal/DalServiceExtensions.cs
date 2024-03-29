using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Dal.Repositories;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeScoutBackend.Dal;

public static class DalServiceExtensions
{
    public static IServiceCollection AddDalServices(
        this IServiceCollection services,
        DatabaseSettings databaseSettings)
    {
        services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(databaseSettings.ConnectionString); });
        
        ConfigureMapping();
        
        services
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IMenuItemRepository, MenuItemRepository>();
        return services;
    }

    private static void ConfigureMapping()
    {
        TypeAdapterConfig<MenuItem, MenuItemEntity>.NewConfig()
            .Map(dest => dest.BeverageTypeEntity, 
                src => src.BeverageType)
            .Map(dest => dest.BeverageTypeEntityId, 
                src => src.BeverageType.Id);

        TypeAdapterConfig<MenuItemEntity, MenuItem>.NewConfig()
            .Map(dest => dest.BeverageType,
                src => src.BeverageTypeEntity);
    }
}