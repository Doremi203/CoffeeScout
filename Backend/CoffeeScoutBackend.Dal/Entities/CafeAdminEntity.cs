using System.ComponentModel.DataAnnotations;

namespace CoffeeScoutBackend.Dal.Entities;

public record CafeAdminEntity
{
    [Key] public string UserId { get; set; } = string.Empty;

    public long CafeId { get; set; }

    public required AppUser User { get; set; }
    public required CafeEntity Cafe { get; set; }
}