namespace CoffeeScoutBackend.Api.Requests.V1.Orders;

public class OrderMenuItemRequest
{
    public required long Id { get; init; }
    public required int Quantity { get; init; }
}