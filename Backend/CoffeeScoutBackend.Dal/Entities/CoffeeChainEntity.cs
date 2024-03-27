namespace CoffeeScoutBackend.Dal.Entities;

public record CoffeeChainEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}