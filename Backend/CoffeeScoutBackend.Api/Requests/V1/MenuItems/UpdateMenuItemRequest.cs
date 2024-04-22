namespace CoffeeScoutBackend.Api.Requests.V1.MenuItems;

public record UpdateMenuItemRequest(
    string Name,
    decimal Price,
    int SizeInMl,
    int BeverageTypeId
);