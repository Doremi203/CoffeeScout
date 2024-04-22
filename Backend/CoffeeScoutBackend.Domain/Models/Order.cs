namespace CoffeeScoutBackend.Domain.Models;

public record Order
{
    public long Id { get; set; }
    public Customer Customer { get; set; }
    public Cafe Cafe { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}