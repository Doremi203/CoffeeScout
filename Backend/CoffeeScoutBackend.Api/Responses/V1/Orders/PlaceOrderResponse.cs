namespace CoffeeScoutBackend.Api.Responses.V1.Orders;

public record PlaceOrderResponse
{
    public required long Id { get; init; }
    public required string CustomerId { get; init; }
    public required long CafeId { get; init; }
    public required DateTime Date { get; init; }
    public required string Status { get; init; }
    public required List<PlaceOrderItemResponse> OrderItems { get; init; }
}