namespace CoffeeScoutBackend.Domain.Models;

public record MenuItem
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int CategoryId { get; init; }
}