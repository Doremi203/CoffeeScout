using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.V1.MenuItems;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.MenuItems)]
public class MenuItemsController(
    IMenuItemService menuItemService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<List<MenuItemResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMenuItemsByBeverageTypeInArea(
        [FromQuery] GetMenuItemsByBeverageTypeInAreaRequest request
    )
    {
        var menuItems = await menuItemService.GetAllInAreaByBeverageType(
            new Location
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            },
            request.RadiusInMeters,
            request.BeverageTypeId);

        return Ok(menuItems.Adapt<IEnumerable<MenuItemResponse>>());
    }

    [HttpPost]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddMenuItem(AddMenuItemRequest request)
    {
        var newMenuItem = new MenuItem
        {
            Name = request.Name,
            Price = request.Price,
            SizeInMl = request.SizeInMl,
            BeverageType = new BeverageType
            {
                Name = request.BeverageTypeName
            }
        };
        var menuItem = await menuItemService.Add(User.GetId(), newMenuItem);

        return Created($"{RoutesV1.MenuItems}/{menuItem.Id}", menuItem.Adapt<MenuItemResponse>());
    }
}