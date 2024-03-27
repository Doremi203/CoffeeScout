namespace CoffeeScoutBackend.Dal.Entities;

public record CafeEntity
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public LocationEntity LocationEntity { get; set; }
    public List<CafeMenuitemEntity> Menuitems {get; set; }
}