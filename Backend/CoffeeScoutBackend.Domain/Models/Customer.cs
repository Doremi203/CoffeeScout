namespace CoffeeScoutBackend.Domain.Models;

public record Customer
{
    public string UserId { get; init; } = string.Empty;
    public ICollection<MenuItem> FavoriteMenuItems { get; init; } = new HashSet<MenuItem>();
}