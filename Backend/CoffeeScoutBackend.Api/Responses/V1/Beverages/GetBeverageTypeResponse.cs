namespace CoffeeScoutBackend.Api.Responses.V1.Beverages;

public record GetBeverageTypeResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}