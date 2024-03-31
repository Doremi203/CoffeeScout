using NetTopologySuite.Geometries;

namespace CoffeeScoutBackend.Dal.Entities;

public record CafeEntity
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public required Point Location { get; set; }

    public ICollection<CafeAdminEntity> Admins { get; set; } = new HashSet<CafeAdminEntity>();
    public ICollection<MenuItemEntity> MenuItems { get; set; } = new HashSet<MenuItemEntity>();
}