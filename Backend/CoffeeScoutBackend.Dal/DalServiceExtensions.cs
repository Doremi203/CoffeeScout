using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeScoutBackend.Dal;

public static class DalServiceExtensions
{
    public static IServiceCollection AddDalServices(
        this IServiceCollection services, 
        DatabaseSettings databaseSettings)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(databaseSettings.ConnectionString);
        });
        return services;
    }
}