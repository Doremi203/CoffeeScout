namespace CoffeeScoutBackend.Domain.Models;

public record MenuItem
{
    public long Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
    public int SizeInMl { get; init; }
    public required BeverageType BeverageType { get; init; }
    public Cafe Cafe { get; init; }
}