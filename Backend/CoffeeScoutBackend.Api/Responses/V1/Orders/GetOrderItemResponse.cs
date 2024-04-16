namespace CoffeeScoutBackend.Api.Responses.V1.Orders;

public record GetOrderItemResponse
{
    public required int Quantity { get; init; }
    public required decimal PricePerItem { get; init; }
    public required GetOrderMenuItemResponse MenuItem { get; init; }
}