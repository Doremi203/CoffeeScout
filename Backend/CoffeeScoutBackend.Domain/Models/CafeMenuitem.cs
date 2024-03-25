namespace CoffeeScoutBackend.Domain.Models;

public record CafeMenuitem : MenuItem
{
    public bool InStock { get; init; }
}