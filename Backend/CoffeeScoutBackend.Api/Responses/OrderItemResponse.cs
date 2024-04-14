using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses;

public record OrderItemResponse
{
    public required int Quantity { get; init; }
    public required decimal PricePerItem { get; init; }
    public required CafeMenuItemResponse MenuItem { get; init; }
}