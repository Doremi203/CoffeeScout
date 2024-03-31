namespace CoffeeScoutBackend.Domain.Models;

public record OrderItem
{
    public Order Order { get; set; }
    public long MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public MenuItem MenuItem { get; set; }
}