using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses;

public record OrderResponse
{
    public required long Id { get; init; }
    public required string CustomerId { get; init; }
    public required DateTime OrderDate { get; init; }
    public required string Status { get; init; }
    public required List<OrderItemResponse> OrderItems { get; init; }

    public record OrderItemResponse
    {
        public long MenuItemId { get; init; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public bool IsCompleted { get; set; }
    }
}