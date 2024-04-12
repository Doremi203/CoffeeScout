namespace CoffeeScoutBackend.Api.Responses;

public record CoffeeChainResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
}