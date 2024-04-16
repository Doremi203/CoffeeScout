using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Requests.V1.Beverages;

public record GetBeverageTypesRequest(
    PaginationModel Pagination
);