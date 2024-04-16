namespace CoffeeScoutBackend.Api.Responses.V1.MenuItems;

public record AddMenuItemResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int SizeInMl { get; init; }
    public required int BeverageTypeId { get; init; }
    public required long CafeId { get; init; }
}