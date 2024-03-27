namespace CoffeeScoutBackend.Domain.Models;

public record Customer
{
    public string UserId { get; init; } = string.Empty;
    public required ICollection<MenuItem> FavoriteMenuItems { get; init; }
}