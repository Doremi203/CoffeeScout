namespace CoffeeScoutBackend.Domain.Models;

public record CafeAdmin
{
    public string Id { get; init; } = string.Empty;
    public required Cafe Cafe { get; init; }
}