namespace CoffeeScoutBackend.Domain.Models;

public record CoffeeChain
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
}