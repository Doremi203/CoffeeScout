namespace CoffeeScoutBackend.Dal.Entities;

public record BeverageTypeEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<MenuItemEntity> MenuItems { get; set; } = new HashSet<MenuItemEntity>();
}