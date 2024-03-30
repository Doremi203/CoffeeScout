using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Dal.Repositories;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
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

        ConfigureMapping();

        services
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IMenuItemRepository, MenuItemRepository>()
            .AddScoped<ICafeRepository, CafeRepository>();
        return services;
    }

    private static void ConfigureMapping()
    {
        TypeAdapterConfig<Cafe, CafeEntity>.NewConfig()
            .MapWith(dest => new CafeEntity
            {
                Id = dest.Id,
                Name = dest.Name,
                Location = GeometryFactory.Default.CreatePoint(
                    new Coordinate(
                        dest.Location.Longitude,
                        dest.Location.Latitude)
                ),
            });
        TypeAdapterConfig<CafeEntity, Cafe>.NewConfig()
            .Map(dest => dest.Location,
                src => new Location
                {
                    Latitude = src.Location.Y,
                    Longitude = src.Location.X
                }
            );
        TypeAdapterConfig<MenuItem, MenuItemEntity>.NewConfig()
            .Map(dest => dest.BeverageTypeId,
                src => src.BeverageType.Id);
    }
}