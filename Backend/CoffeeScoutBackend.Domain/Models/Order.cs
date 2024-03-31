namespace CoffeeScoutBackend.Domain.Models;

public record Order
{
    public long Id { get; set; }
    public Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}