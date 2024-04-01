using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrdersController(
    IOrderService orderService
) : ControllerBase
{
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