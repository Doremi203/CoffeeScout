using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Requests.V1.MenuItems;

public record GetMenuItemsByBeverageTypeInAreaRequest(
    Location Location,
    double RadiusInMeters,
    long BeverageTypeId
);