using CoffeeScoutBackend.Domain.ServiceModels;

namespace CoffeeScoutBackend.Api.Requests.V1.CoffeeChains;

public record GetCoffeeChainsRequest(
    PaginationModel Pagination
);