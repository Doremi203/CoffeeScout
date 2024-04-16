using CoffeeScoutBackend.Api.Responses.V1.Cafes;

namespace CoffeeScoutBackend.Api.Responses;

public record MenuItemResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int SizeInMl { get; init; }
    public required BeverageTypeResponse BeverageType { get; init; }
    public required GetCafeResponse GetCafe { get; init; }
}