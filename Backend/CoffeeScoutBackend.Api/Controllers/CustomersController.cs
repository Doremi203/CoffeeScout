using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using CoffeeScoutBackend.Api.Requests.V1.Customers;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.Customers)]
[Authorize(Roles = nameof(Roles.Customer))]
public class CustomersController(
    ICustomerService customerService,
    IOrderService orderService
) : ControllerBase
{
    [HttpGet("info")]
    [ProducesResponseType<CustomerInfoResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInfo()
    {
        var customer = await customerService.GetInfo(User.GetId());

        return Ok(customer.Adapt<CustomerInfoResponse>());
    }

    [HttpPatch("info")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateInfo(UpdateCustomerInfoRequest request)
    {
        await customerService.UpdateInfo(User.GetId(), request.Adapt<CustomerInfo>());

        return NoContent();
    }

    [HttpPost("favored-menu-items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddFavoredMenuItem([FromQuery] long menuItemId)
    {
        await customerService.AddFavoredMenuItem(User.GetId(), menuItemId);
        
        return Created($"{RoutesV1.Customers}/favored-menu-items/{menuItemId}", null);
    }
    
    [HttpGet("favored-menu-items")]
    [ProducesResponseType<IReadOnlyCollection<MenuItemResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFavoredMenuItems()
    {
        var favoredMenuItems =
            await customerService.GetFavoredMenuItems(User.GetId());
        
        return Ok(favoredMenuItems.Adapt<IReadOnlyCollection<MenuItemResponse>>());
    }

    [HttpDelete("favored-menu-items/{menuItemId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveFavoredMenuItem(long menuItemId)
    {
        await customerService.RemoveFavoredMenuItem(User.GetId(), menuItemId);
        
        return NoContent();
    }

    [HttpGet("favored-beverage-types")]
    [ProducesResponseType<IReadOnlyCollection<BeverageType>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFavoredBeverageTypes()
    {
        var favoredBeverageTypes =
            await customerService.GetFavoredBeverageTypes(User.GetId());
        
        return Ok(favoredBeverageTypes);
    }
    
    [HttpGet("orders")]
    [ProducesResponseType<IReadOnlyCollection<OrderResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders([FromQuery] GetOrdersRequest request)
    {
        var orders = 
            await orderService.GetCustomerOrders(
                User.GetId(), request.Status, request.From);

        return Ok(orders.Adapt<IReadOnlyCollection<OrderResponse>>());
    }
}