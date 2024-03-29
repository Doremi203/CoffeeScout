namespace CoffeeScoutBackend.Domain.Models;

public record CafeAdmin
{
    public string UserId { get; init; } = string.Empty;
    public required Cafe Cafe { get; init; }
}