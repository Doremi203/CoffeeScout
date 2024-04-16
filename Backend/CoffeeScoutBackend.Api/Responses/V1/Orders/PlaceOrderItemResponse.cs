namespace CoffeeScoutBackend.Api.Responses.V1.Orders;

public record PlaceOrderItemResponse
{
    public required int Quantity { get; init; }
    public required decimal PricePerItem { get; init; }
    public required long MenuItemId { get; init; }
}