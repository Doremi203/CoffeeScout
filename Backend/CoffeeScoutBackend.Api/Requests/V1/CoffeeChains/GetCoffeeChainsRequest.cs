namespace CoffeeScoutBackend.Api.Requests.V1.CoffeeChains;

public record GetCoffeeChainsRequest(
    int PageSize,
    int PageNumber
);