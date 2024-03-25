namespace CoffeeScoutBackend.Domain.Models;

public record OrderMenuItem : MenuItem
{
    public int Quantity { get; init; }
    public decimal PricePerItem { get; init; }
}