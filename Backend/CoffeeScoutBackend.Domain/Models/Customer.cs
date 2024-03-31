namespace CoffeeScoutBackend.Domain.Models;

public record Customer
{
    public string Id { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public IReadOnlyCollection<MenuItem> FavoriteMenuItems { get; init; } = new HashSet<MenuItem>();
}