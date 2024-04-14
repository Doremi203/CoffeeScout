namespace CoffeeScoutBackend.Api.Requests.V1.Beverages;

public record GetBeverageTypesRequest(
    int PageSize,
    int PageNumber
);