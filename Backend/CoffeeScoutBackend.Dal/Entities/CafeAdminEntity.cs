namespace CoffeeScoutBackend.Dal.Entities;

public record CafeAdminEntity
{
    public string UserId { get; set; } = string.Empty;
    public long CafeId { get; set; }
    
    public required AppUser User { get; set; }
    public required CafeEntity Cafe { get; set; }
}