namespace CoffeeScoutBackend.Domain.Models;

public record CreateOrderData
{
    public class MenuItemData
    {
        public required long Id { get; init; }
        public required int Quantity { get; init; }
    }
    public required string CustomerId { get; init; }
    public required IReadOnlyCollection<MenuItemData> MenuItems { get; init; }
}