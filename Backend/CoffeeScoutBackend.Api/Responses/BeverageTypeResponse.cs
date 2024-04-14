namespace CoffeeScoutBackend.Api.Responses;

public record BeverageTypeResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}