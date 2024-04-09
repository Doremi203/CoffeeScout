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
    [HttpPost]
    [Authorize(Roles = nameof(Roles.Customer))]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PlaceOrder(PlaceOrderRequest request)
    {
        var orderData = new CreateOrderData
        {
            CustomerId = User.GetId(),
            MenuItems = request.MenuItems
                .Adapt<IReadOnlyCollection<CreateOrderData.MenuItemData>>()
        };
        var order = await orderService.CreateOrder(orderData);

        return Created($"{RoutesV1.Orders}/{order.Id}", order.Adapt<OrderResponse>());
    }
    
    [HttpPatch("{id:long}/complete")]
    [Authorize(Roles = nameof(Roles.CafeAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CompleteCafeOrderPart(long id)
    {
        await orderService.CompleteCafeOrderPart(User.GetId(), id);

        return NoContent();
    }
    
}