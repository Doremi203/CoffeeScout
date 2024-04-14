namespace CoffeeScoutBackend.Domain.Models;

public record AddMenuItemModel
{
    public required string CafeAdminId { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int SizeInMl { get; init; }
    public required int BeverageTypeId { get; init; }
}