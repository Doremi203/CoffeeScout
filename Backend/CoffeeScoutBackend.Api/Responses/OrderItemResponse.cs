using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses;

public record OrderItemResponse
{
    public required MenuItemResponse MenuItem { get; init; }
    public required int Quantity { get; init; }
    public required decimal PricePerItem { get; init; }

    public record MenuItemResponse
    {
        public required string Name { get; init; }
        public required int SizeInMl { get; init; }
        public required BeverageTypeResponse BeverageType { get; init; }
    }
}