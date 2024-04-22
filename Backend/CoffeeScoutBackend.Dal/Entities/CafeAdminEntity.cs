using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeScoutBackend.Dal.Entities;

public record CafeAdminEntity
{
    [ForeignKey(nameof(User))] public string Id { get; set; } = string.Empty;

    public long CafeId { get; set; }

    public required AppUser User { get; set; }
    public required CafeEntity Cafe { get; set; }
}