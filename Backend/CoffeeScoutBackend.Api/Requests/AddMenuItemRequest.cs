namespace CoffeeScoutBackend.Api.Requests;

public record AddMenuItemRequest(
    string Name,
    decimal Price,
    string BeverageTypeName
);