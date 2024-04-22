namespace CoffeeScoutBackend.Domain.ServiceModels;

public record UpdateMenuItemModel
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int SizeInMl { get; init; }
    public required int BeverageTypeId { get; init; }
}