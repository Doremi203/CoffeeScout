namespace CoffeeScoutBackend.Domain.Models;

public record Cafe
{
    public long Id { get; init; }
    public string? Name { get; init; }
    public Location Location { get; init; }
    public IReadOnlyCollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
    public IReadOnlyCollection<CafeAdmin> Admins { get; set; } = new HashSet<CafeAdmin>();
}