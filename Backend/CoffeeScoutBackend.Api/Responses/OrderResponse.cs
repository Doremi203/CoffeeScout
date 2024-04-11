namespace CoffeeScoutBackend.Api.Responses;

public record OrderResponse
{
    public required long Id { get; init; }
    public required string CustomerId { get; init; }
    public required CafeResponse Cafe { get; init; }
    public required DateTime Date { get; init; }
    public required string Status { get; init; }
    public required List<OrderItemResponse> OrderItems { get; init; }
}