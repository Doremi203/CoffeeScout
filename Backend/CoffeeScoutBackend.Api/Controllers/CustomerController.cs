using System.Security.Claims;
using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
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
    public async Task<IActionResult> AddFavoredMenuItemAsync(long menuItemId)
    {
        await customerService.AddFavoredMenuItemAsync(CurrentUserId, menuItemId);
        return Ok();
    }

    [HttpGet("favored-beverage-types")]
    public async Task<IActionResult> GetFavoredBeverageTypesAsync()
    {
        var favoredBeverageTypes =
            await customerService.GetFavoredBeverageTypesAsync(CurrentUserId);
        return Ok(favoredBeverageTypes);
    }

    [HttpPost("orders")]
    public async Task<IActionResult> PlaceOrderAsync(PlaceOrderRequest request)
    {
        //await orderService.CreateOrderAsync();
        return Ok();
    }
}