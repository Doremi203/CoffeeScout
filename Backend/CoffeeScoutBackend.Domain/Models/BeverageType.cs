namespace CoffeeScoutBackend.Domain.Models;

public record BeverageType
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}