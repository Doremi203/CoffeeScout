using System.Security.Claims;
using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = nameof(Roles.Customer))]
public class CustomerController(
    ICustomerService customerService,
    IOrderService orderService
) : ControllerBase
{
    private string CurrentUserId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("User ID not found");

    [HttpPost("favored-menu-items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddFavoredMenuItemAsync(long menuItemId)
    {
        await customerService.AddFavoredMenuItemAsync(CurrentUserId, menuItemId);
        return Created($"api/v1/customers/favored-menu-items/{menuItemId}", null);
    }

    [HttpGet("favored-beverage-types")]
    [ProducesResponseType<IReadOnlyCollection<BeverageType>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFavoredBeverageTypesAsync()
    {
        var favoredBeverageTypes =
            await customerService.GetFavoredBeverageTypesAsync(CurrentUserId);
        return Ok(favoredBeverageTypes);
    }

    [HttpPost("orders")]
    [ProducesResponseType<long>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PlaceOrderAsync(PlaceOrderRequest request)
    {
        var orderData = new CreateOrderData
        {
            CustomerId = CurrentUserId,
            MenuItems = request.MenuItems
                .Adapt<IReadOnlyCollection<CreateOrderData.MenuItemData>>()
        };
        var id = await orderService.CreateOrderAsync(orderData);
        return Created($"api/v1/orders/{id}", id);
    }
}