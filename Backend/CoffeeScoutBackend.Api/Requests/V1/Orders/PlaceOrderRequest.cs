namespace CoffeeScoutBackend.Api.Requests.V1.Orders;

public record PlaceOrderRequest
{
    public required List<OrderMenuItemRequest> MenuItems { get; init; }
}