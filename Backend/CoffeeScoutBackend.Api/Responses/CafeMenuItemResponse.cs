using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses;

public record CafeMenuItemResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required int SizeInMl { get; init; }
    public required BeverageType BeverageType { get; init; }
}