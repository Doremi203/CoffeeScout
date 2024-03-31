using System.Security.Claims;
using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/cafes")]
public class CafeController(
    ICafeService cafeService,
    IOrderService orderService
) : ControllerBase
{
    private string CurrentCafeAdminId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("Cafe admin ID not found");

    [HttpGet]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<List<CafeResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafesAsync(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        [FromQuery] double radius)
    {
        var cafes =
            await cafeService.GetCafesInAreaAsync(
                new Location { Longitude = longitude, Latitude = latitude },
                radius);

        return Ok(cafes.Adapt<IReadOnlyCollection<CafeResponse>>());
    }

    [HttpPost("menu-items")]
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
        await cafeService.AddMenuItemAsync(CurrentCafeAdminId, newMenuItem);

        return Created();
    }

    [HttpGet("orders")]
    [ProducesResponseType<List<OrderResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersAsync(
        [FromQuery] OrderStatus status,
        [FromQuery] DateTime from
    )
    {
        var orders = await orderService.GetCafeOrdersAsync(CurrentCafeAdminId, status, from);

        return Ok(orders.Adapt<IReadOnlyCollection<OrderResponse>>());
    }
}