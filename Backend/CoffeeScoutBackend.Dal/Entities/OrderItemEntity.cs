namespace CoffeeScoutBackend.Dal.Entities;

public record OrderItemEntity
{
    public long OrderId { get; set; }
    public long MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerItem { get; set; }
    public bool IsCompleted { get; set; }

    public OrderEntity Order { get; set; }
    public MenuItemEntity MenuItem { get; set; }
}