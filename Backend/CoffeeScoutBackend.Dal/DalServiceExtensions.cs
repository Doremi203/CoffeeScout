using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Dal.Repositories;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Location = CoffeeScoutBackend.Domain.Models.Location;

namespace CoffeeScoutBackend.Dal;

public static class DalServiceExtensions
{
    public static IServiceCollection AddDalServices(
        this IServiceCollection services,
        DatabaseSettings databaseSettings)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options
                .UseNpgsql(
                    databaseSettings.ConnectionString,
                    o => o.UseNetTopologySuite())
                .UseSnakeCaseNamingConvention();
        });

        services
            .AddSingleton<ILocationProvider, GpsLocationProvider>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IMenuItemRepository, MenuItemRepository>()
            .AddScoped<ICafeRepository, CafeRepository>();
        
        var locationProvider = services.BuildServiceProvider().GetRequiredService<ILocationProvider>();
        ConfigureMapping(locationProvider);
        
        return services;
    }

    private static void ConfigureMapping(ILocationProvider locationProvider)
    {
        TypeAdapterConfig<Cafe, CafeEntity>.NewConfig()
            .PreserveReference(true)
            .MapWith(dest => new CafeEntity
            {
                Id = dest.Id,
                Name = dest.Name,
                Location = locationProvider.CreatePoint(
                    dest.Location.Latitude, dest.Location.Longitude),
                Admins = dest.Admins.Adapt<ICollection<CafeAdminEntity>>(),
                MenuItems = dest.MenuItems.Adapt<ICollection<MenuItemEntity>>()
            });
        TypeAdapterConfig<CafeEntity, Cafe>.NewConfig()
            .PreserveReference(true)
            .Map(dest => dest.Location,
                src => new Location
                {
                    Latitude = src.Location.Y,
                    Longitude = src.Location.X
                }
            );
        TypeAdapterConfig<MenuItem, MenuItemEntity>.NewConfig()
            .PreserveReference(true)
            .Map(dest => dest.BeverageTypeId,
                src => src.BeverageType.Id);
    }
}