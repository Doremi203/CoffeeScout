namespace CoffeeScoutBackend.Dal.Entities;

public record CustomerEntity
{
    public string UserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    
    public required AppUser User { get; set; }
    public required ICollection<MenuItemEntity> FavoriteItems { get; set; }
}