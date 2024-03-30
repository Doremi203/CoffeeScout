namespace CoffeeScoutBackend.Domain.Models;

public record Location
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}