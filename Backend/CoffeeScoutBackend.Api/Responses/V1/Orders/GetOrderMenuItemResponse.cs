using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses.V1.Orders;

public record GetOrderMenuItemResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int SizeInMl { get; init; }
    public required BeverageType BeverageType { get; init; }
}