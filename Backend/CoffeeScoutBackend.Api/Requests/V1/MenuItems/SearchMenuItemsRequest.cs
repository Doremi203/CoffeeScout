namespace CoffeeScoutBackend.Api.Requests.V1.MenuItems;

public record SearchMenuItemsRequest(
    string Name,
    int Limit
);