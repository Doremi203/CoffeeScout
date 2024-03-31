using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Authorize(Roles = nameof(Roles.Customer))]
[Route("api/v1/[controller]")]
public class MenuItemsController(
    IMenuItemService menuItemService
) : ControllerBase
{
    [HttpGet]
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