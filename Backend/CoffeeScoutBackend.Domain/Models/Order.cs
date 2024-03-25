namespace CoffeeScoutBackend.Domain.Models;

public record Order
{
    public long Id { get; init; }
    public long CustomerId { get; init; }
    public DateTime OrderDate { get; init; }
    public OrderStatus Status { get; init; }
    public List<OrderMenuItem> OrderItems { get; init; }
}