namespace CoffeeScoutBackend.Domain.Models;

public record Cafe
{
    public long Id { get; init; }
    public required string Name { get; init; }
    public required Location Location { get; init; }
    public required string Address { get; init; }
    public CoffeeChain CoffeeChain { get; init; }
    public IReadOnlyCollection<WorkingHours> WorkingHours { get; init; } = new HashSet<WorkingHours>();
    public IReadOnlyCollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
    public IReadOnlyCollection<CafeAdmin> Admins { get; set; } = new HashSet<CafeAdmin>();
}