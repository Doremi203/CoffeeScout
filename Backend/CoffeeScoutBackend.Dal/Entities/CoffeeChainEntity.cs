namespace CoffeeScoutBackend.Dal.Entities;

public record CoffeeChainEntity
{
    public long Id { get; set; }
    public required string Name { get; set; }
}