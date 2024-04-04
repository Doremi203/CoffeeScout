using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Dal.Entities;

public record OrderEntity
{
    public long Id { get; set; }
    public string CustomerId { get; set; }
    public long StatusId { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
    
    public CustomerEntity Customer { get; set; }
    public List<OrderItemEntity> OrderItems { get; set; }
}