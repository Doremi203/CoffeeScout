namespace CoffeeScoutBackend.Domain.Models;

public record BeverageType
{
    public long Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}