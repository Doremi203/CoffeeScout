using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses;

public record MenuItemResponse
{
    public class CafeResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public Location Location { get; set; }
    }
    public long Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
    public required BeverageType BeverageType { get; init; }
    public required CafeResponse Cafe { get; init; }
}