using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses;

public record MenuItemResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required BeverageType BeverageType { get; init; }
    public required CafeResponse Cafe { get; init; }
}