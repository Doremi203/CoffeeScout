using CoffeeScoutBackend.Api.Responses.V1.Orders;
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
    [ProducesResponseType<GetOrderResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder([FromRoute] long id)
    {
        var order = await orderService.GetById(id);

        return Ok(order.Adapt<GetOrderResponse>());
    }
}