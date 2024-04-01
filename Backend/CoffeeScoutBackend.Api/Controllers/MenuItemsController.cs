using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests;
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
    IMenuItemService menuItemService,
    ICafeService cafeService
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
    
    [HttpPost]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddMenuItemAsync(AddMenuItemRequest request)
    {
        var newMenuItem = new MenuItem
        {
            Name = request.Name,
            Price = request.Price,
            BeverageType = new BeverageType
            {
                Name = request.BeverageTypeName
            }
        };
        await cafeService.AddMenuItemAsync(User.GetId(), newMenuItem);

        return Created();
    }
}