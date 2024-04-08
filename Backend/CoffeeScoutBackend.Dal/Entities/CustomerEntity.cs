using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeScoutBackend.Dal.Entities;

public record CustomerEntity
{ 
    [ForeignKey(nameof(User))]
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;

    public required AppUser User { get; set; }
    public required ICollection<MenuItemEntity> FavoriteMenuItems { get; set; }
    public required ICollection<OrderEntity> Orders { get; set; }
    public required ICollection<ReviewEntity> Reviews { get; set; }
}