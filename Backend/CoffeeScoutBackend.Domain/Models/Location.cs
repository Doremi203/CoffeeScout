namespace CoffeeScoutBackend.Domain.Models;

public record struct Location
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}