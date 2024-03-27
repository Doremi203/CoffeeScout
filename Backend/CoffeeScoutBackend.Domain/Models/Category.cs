namespace CoffeeScoutBackend.Domain.Models;

public record Category
{
    public long Id { get; init; }
    public required string Name { get; init; }
}