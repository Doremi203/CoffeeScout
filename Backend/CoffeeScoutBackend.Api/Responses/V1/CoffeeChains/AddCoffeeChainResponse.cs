namespace CoffeeScoutBackend.Api.Responses.V1.CoffeeChains;

public record AddCoffeeChainResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
}