namespace CoffeeScoutBackend.Dal.Entities;

public record OrderEntity
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatusEntity StatusEntity { get; set; }
    public List<OrderItemEntity> OrderItems { get; set; }
}