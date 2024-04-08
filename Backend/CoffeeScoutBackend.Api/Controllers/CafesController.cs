using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.Cafes)]
public class CafesController(
    ICafeService cafeService,
    IOrderService orderService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<List<CafeResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafes([FromQuery] GetCafesRequest request)
    {
        var cafes =
            await cafeService.GetCafesInArea(
                new Location
                {
                    Longitude = request.Longitude,
                    Latitude = request.Latitude
                },
                request.RadiusInMeters);

        return Ok(cafes.Adapt<IReadOnlyCollection<CafeResponse>>());
    }

    [HttpPost]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType<CafeResponse>(StatusCodes.Status201Created)]
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

        var newCafe = await cafeService.AddCafe(cafe);

        return Created($"{RoutesV1.Cafes}/{newCafe.Id}", newCafe.Adapt<CafeResponse>());
    }

    [HttpPatch]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateCafe(UpdateCafeRequest request)
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

        await cafeService.UpdateCafe(User.GetId(), cafe);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCafe([FromRoute] long id)
    {
        await cafeService.DeleteCafe(id);

        return NoContent();
    }

    [HttpGet("orders")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType<List<OrderResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCafeOrders([FromQuery] GetCafeOrdersRequest request)
    {
        var orders =
            await orderService.GetCafeOrders(
                User.GetId(), request.Status, request.From);

        return Ok(orders.Adapt<IReadOnlyCollection<OrderResponse>>());
    }
}