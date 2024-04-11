namespace CoffeeScoutBackend.Api.Requests.V1.MenuItems;

public record UpdateMenuItemRequest(
    string Name,
    string Description,
    decimal Price,
    int SizeInMl,
    string BeverageTypeName
);