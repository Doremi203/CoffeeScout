using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/cafes")]
public class CafesController(
    ICafeService cafeService,
    IOrderService orderService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<List<CafeResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafes(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        [FromQuery] double radius)
    {
        var cafes =
            await cafeService.GetCafesInArea(
                new Location { Longitude = longitude, Latitude = latitude },
                radius);

        return Ok(cafes.Adapt<IReadOnlyCollection<CafeResponse>>());
    }
    
    [HttpGet("orders")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType<List<OrderResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders(
        [FromQuery] OrderStatus status,
        [FromQuery] DateTime from
    )
    {
        var orders = 
            await orderService.GetCafeOrders(
                User.GetId(), status, from);

        return Ok(orders.Adapt<IReadOnlyCollection<OrderResponse>>());
    }
}