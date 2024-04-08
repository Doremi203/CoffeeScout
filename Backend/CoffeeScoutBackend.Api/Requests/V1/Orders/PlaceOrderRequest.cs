namespace CoffeeScoutBackend.Api.Requests.V1.Orders;

public record PlaceOrderRequest
{
    public required List<MenuItemRequest> MenuItems { get; init; }

    public class MenuItemRequest
    {
        public required long Id { get; init; }
        public required int Quantity { get; init; }
    }
}