using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.V1.Orders;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.Orders)]
public class OrdersController(
    IOrderService orderService
) : ControllerBase
{
    [HttpGet("{id:long}")]
    [Authorize(Roles = $"{nameof(Roles.Customer)},{nameof(Roles.CafeAdmin)},{nameof(Roles.SuperAdmin)}")]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrder(long id)
    {
        var order = await orderService.GetById(id);

        return Ok(order.Adapt<OrderResponse>());
    }
    
    [HttpPatch("{id:long}/cancel")]
    [Authorize(Roles = $"{nameof(Roles.Customer)},{nameof(Roles.CafeAdmin)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CancelOrder(long id)
    {
        await orderService.CancelOrder(id);

        return NoContent();
    }
    
    [HttpPatch("{id:long}/pay")]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PayOrder(long id)
    {
        await orderService.PayOrder(User.GetId(), id);

        return NoContent();
    }
}