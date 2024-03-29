namespace CoffeeScoutBackend.Domain.Models;

public record Cafe
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public Location Location { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
}