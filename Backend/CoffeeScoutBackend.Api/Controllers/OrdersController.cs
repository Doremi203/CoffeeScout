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
}