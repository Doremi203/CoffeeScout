namespace CoffeeScoutBackend.Dal.Entities;

public record OrderItemEntity : MenuItemEntity
{
    public int Quantity { get; set; }
    public decimal PricePerItem { get; set; }
}