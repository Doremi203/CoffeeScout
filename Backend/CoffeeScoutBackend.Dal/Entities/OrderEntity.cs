using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Dal.Entities;

public record OrderEntity
{
    public long Id { get; set; }
    public string CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus StatusEntity { get; set; }

    public CustomerEntity Customer { get; set; }
    public List<OrderItemEntity> OrderItems { get; set; }
}