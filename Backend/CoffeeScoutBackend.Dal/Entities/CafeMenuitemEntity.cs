namespace CoffeeScoutBackend.Dal.Entities;

public record CafeMenuitemEntity : MenuItemEntity
{
    public bool InStock { get; set; }
}