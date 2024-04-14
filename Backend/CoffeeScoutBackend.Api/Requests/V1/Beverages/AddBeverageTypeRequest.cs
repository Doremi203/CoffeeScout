namespace CoffeeScoutBackend.Api.Requests.V1.Beverages;

public record AddBeverageTypeRequest(
    string Name,
    string Description
);