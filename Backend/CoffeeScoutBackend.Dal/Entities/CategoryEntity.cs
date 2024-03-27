namespace CoffeeScoutBackend.Dal.Entities;

public record CategoryEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public required ICollection<MenuItemEntity> MenuItems { get; set; }
}