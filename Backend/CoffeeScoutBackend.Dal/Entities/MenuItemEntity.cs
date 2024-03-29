namespace CoffeeScoutBackend.Dal.Entities;

public record MenuItemEntity
{
    public long Id { get; set; }
    public int BeverageTypeId { get; set; }
    public long CafeId { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }

    public required BeverageTypeEntity BeverageType { get; set; }
    public CafeEntity Cafe { get; set; }
    public ICollection<CustomerEntity> CustomersFavoredBy { get; set; } = new HashSet<CustomerEntity>();
}