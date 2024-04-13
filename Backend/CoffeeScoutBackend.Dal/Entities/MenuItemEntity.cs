using NpgsqlTypes;

namespace CoffeeScoutBackend.Dal.Entities;

public record MenuItemEntity
{
    public long Id { get; set; }
    public int BeverageTypeId { get; set; }
    public long CafeId { get; set; }
    public required string Name { get; set; }
    public NpgsqlTsVector SearchVector { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int SizeInMl { get; set; }

    public required BeverageTypeEntity BeverageType { get; set; }
    public required CafeEntity Cafe { get; set; }
    public required ICollection<CustomerEntity> CustomersFavoredBy { get; set; }
    public required ICollection<ReviewEntity> Reviews { get; set; }
}