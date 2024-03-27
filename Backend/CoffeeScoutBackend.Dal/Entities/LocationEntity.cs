namespace CoffeeScoutBackend.Dal.Entities;

public record struct LocationEntity
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}