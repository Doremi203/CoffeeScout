namespace CoffeeScoutBackend.Api.Requests.V1.MenuItems;

public record GetMenuItemsByBeverageTypeInAreaRequest(
    double Latitude,
    double Longitude,
    double RadiusInMeters,
    long BeverageTypeId
);