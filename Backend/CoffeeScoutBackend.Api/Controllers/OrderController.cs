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
[Route("api/v1/orders")]
public class OrderController(
    IOrderService orderService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType<List<OrderResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersAsync(
        [FromQuery] OrderStatus status,
        [FromQuery] DateTime from
    )
    {
        var orders = 
            await orderService.GetCafeOrdersAsync(
                User.GetId(), status, from);

        return Ok(orders.Adapt<IReadOnlyCollection<OrderResponse>>());
    }
    
    [HttpPost]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<long>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PlaceOrderAsync(PlaceOrderRequest request)
    {
        var orderData = new CreateOrderData
        {
            CustomerId = User.GetId(),
            MenuItems = request.MenuItems
                .Adapt<IReadOnlyCollection<CreateOrderData.MenuItemData>>()
        };
        var id = await orderService.CreateOrderAsync(orderData);
        return Created($"api/v1/orders/{id}", id);
    }
}