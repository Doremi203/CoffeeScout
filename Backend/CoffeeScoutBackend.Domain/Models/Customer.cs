namespace CoffeeScoutBackend.Domain.Models;

public record Customer
{
    public long Id { get; init; }
    public required List<MenuItem> FavoriteItems { get; init; }
}