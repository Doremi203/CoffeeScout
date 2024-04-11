namespace CoffeeScoutBackend.Domain.Models;

public record OrderItem
{
    public Order Order { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerItem { get; set; }
    public required MenuItem MenuItem { get; set; }
}