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
        [FromQuery] double radiusInMeters)
    {
        var cafes =
            await cafeService.GetCafesInArea(
                new Location { Longitude = longitude, Latitude = latitude },
                radiusInMeters);

        return Ok(cafes.Adapt<IReadOnlyCollection<CafeResponse>>());
    }
    
    [HttpPost]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCafe(AddCafeRequest request)
    {
        var cafe = new Cafe
        {
            Name = request.Name,
            Location = new Location
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            }
        };
        
        await cafeService.AddCafe(cafe);

        return Created();
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