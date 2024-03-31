using System.ComponentModel.DataAnnotations;

namespace CoffeeScoutBackend.Dal.Entities;

public record CustomerEntity
{
    [Key] public string UserId { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public required AppUser User { get; set; }
    public required ICollection<MenuItemEntity> FavoriteMenuItems { get; set; }
    public required ICollection<OrderEntity> Orders { get; set; }
}