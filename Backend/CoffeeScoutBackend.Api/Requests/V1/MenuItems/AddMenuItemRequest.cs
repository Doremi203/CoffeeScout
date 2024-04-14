namespace CoffeeScoutBackend.Api.Requests.V1.MenuItems;

public record AddMenuItemRequest(
    string Name,
    decimal Price,
    int SizeInMl,
    int BeverageTypeId
);