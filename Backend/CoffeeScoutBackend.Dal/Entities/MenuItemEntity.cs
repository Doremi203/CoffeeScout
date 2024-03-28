namespace CoffeeScoutBackend.Dal.Entities;

public record MenuItemEntity
{
    public long Id { get; set; }
    public int CategoryId { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    
    public required BeverageTypeEntity BeverageTypeEntity { get; set; }
    public required ICollection<CustomerEntity> CustomersFavoredBy { get; set; }
}