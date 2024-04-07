namespace CoffeeScoutBackend.Api.Requests;

public record AddMenuItemRequest(
    string Name,
    decimal Price,
    int SizeInMl,
    string BeverageTypeName
);