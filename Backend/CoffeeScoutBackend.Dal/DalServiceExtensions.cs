using CoffeeScoutBackend.Dal.Config;
using CoffeeScoutBackend.Dal.Infrastructure;
using CoffeeScoutBackend.Dal.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
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
            .AddScoped<IReviewRepository, ReviewRepository>()
            .AddScoped<IBeverageTypeRepository, BeverageTypeRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IMenuItemRepository, MenuItemRepository>()
            .AddScoped<ICafeRepository, CafeRepository>();

        services
            .AddSingleton<ILocationProvider, GpsLocationProvider>();

        var locationProvider = services.BuildServiceProvider().GetRequiredService<ILocationProvider>();
        ConfigureMapping(locationProvider);

        return services;
    }

    private static void ConfigureMapping(ILocationProvider locationProvider)
    {
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
        
        TypeAdapterConfig<Point, Location>.NewConfig()
            .MapWith(dest => 
                new Location { Latitude = dest.Y, Longitude = dest.X });
        TypeAdapterConfig<Location, Point>.NewConfig()
            .MapWith(dest => 
                locationProvider.CreatePoint(dest.Latitude, dest.Longitude));
    }
}