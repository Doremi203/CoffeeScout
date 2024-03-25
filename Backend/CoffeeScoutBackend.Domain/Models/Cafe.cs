namespace CoffeeScoutBackend.Domain.Models;

public record Cafe
{
    public long Id { get; init; }
    public string? Name { get; init; }
    public Location Location { get; init; }
    public List<CafeMenuitem> Menuitems {get; init; }
}