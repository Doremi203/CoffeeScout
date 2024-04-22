using NetTopologySuite.Geometries;

namespace CoffeeScoutBackend.Dal.Entities;

public record CafeEntity
{
    public long Id { get; set; }
    public long CoffeeChainId { get; set; }
    public required string Name { get; set; }
    public required Point Location { get; set; }
    public required string Address { get; set; }

    public required CoffeeChainEntity CoffeeChain { get; set; }
    public ICollection<WorkingHoursEntity> WorkingHours { get; set; } = new HashSet<WorkingHoursEntity>();
    public ICollection<CafeAdminEntity> Admins { get; set; } = new HashSet<CafeAdminEntity>();
    public ICollection<MenuItemEntity> MenuItems { get; set; } = new HashSet<MenuItemEntity>();
}