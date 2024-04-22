namespace CoffeeScoutBackend.Domain.Models;

public record CreateOrderData
{
    public record MenuItemData
    {
        public required long Id { get; init; }
        public required int Quantity { get; init; }
    }
    public required string CustomerId { get; init; }
    public required long CafeId { get; init; }
    public required IReadOnlyCollection<MenuItemData> MenuItems { get; init; }
}