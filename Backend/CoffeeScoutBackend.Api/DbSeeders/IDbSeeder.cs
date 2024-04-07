namespace CoffeeScoutBackend.Api.DbSeeders;

public interface IDbSeeder
{
    public Task SeedDbAsync();
}