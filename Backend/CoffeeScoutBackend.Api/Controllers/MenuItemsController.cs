using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/menu-items")]
public class MenuItemsController(
    IMenuItemService menuItemService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<List<MenuItemResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMenuItemsInAsync(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        [FromQuery] double radiusInMeters,
        [FromQuery] long beverageTypeId
    )
    {
        var menuItems = await menuItemService.GetAllInAreaByBeverageTypeAsync(
            new Location
            {
                Latitude = latitude,
                Longitude = longitude
            },
            radiusInMeters,
            beverageTypeId);

        return Ok(menuItems.Adapt<IEnumerable<MenuItemResponse>>());
    }
}