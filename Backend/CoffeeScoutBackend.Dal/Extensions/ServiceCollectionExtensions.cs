using CoffeeScoutBackend.Dal.Config;
using CoffeeScoutBackend.Dal.Infrastructure;
using CoffeeScoutBackend.Dal.Repositories;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using Location = CoffeeScoutBackend.Domain.Models.Location;

namespace CoffeeScoutBackend.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(
            configuration.GetSection(nameof(DatabaseSettings)));
        
        var databaseSettings = configuration
            .GetRequiredSection(nameof(DatabaseSettings))
            .Get<DatabaseSettings>()!;

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
            .AddScoped<ICoffeeChainRepository, CoffeeChainRepository>()
            .AddScoped<ICafeRepository, CafeRepository>();

        services
            .AddSingleton<ILocationProvider, GpsLocationProvider>();

        services.ConfigureMapsterMapping();

        return services;
    }

    private static void ConfigureMapsterMapping(this IServiceCollection services)
    {
        var locationProvider = services.BuildServiceProvider().GetRequiredService<ILocationProvider>();
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

        TypeAdapterConfig<Point, Location>.NewConfig()
            .MapWith(dest =>
                new Location { Latitude = dest.Y, Longitude = dest.X });
        TypeAdapterConfig<Location, Point>.NewConfig()
            .MapWith(dest =>
                locationProvider.CreatePoint(dest.Latitude, dest.Longitude));
    }
}