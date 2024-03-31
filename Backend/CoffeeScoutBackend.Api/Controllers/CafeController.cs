using System.Security.Claims;
using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Api.Responses;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = nameof(Roles.CafeAdmin))]
public class CafeController(
    ICafeService cafeService,
    IOrderService orderService
) : ControllerBase
{
    private string CurrentCafeAdminId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("Cafe admin ID not found");

    [HttpPost("menu-items")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddMenuItemAsync(AddMenuItemRequest request)
    {
        var newMenuItem = new MenuItem
        {
            Name = request.Name,
            Price = request.Price,
            BeverageType = new BeverageType
            {
                Name = request.BeverageTypeName
            }
        };
        await cafeService.AddMenuItemAsync(CurrentCafeAdminId, newMenuItem);
        
        return Created();
    }

    [HttpGet("orders")]
    [ProducesResponseType<List<OrderResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersAsync(
        [FromQuery] OrderStatus status,
        [FromQuery] DateTime from
    )
    {
        var orders = await orderService.GetCafeOrdersAsync(CurrentCafeAdminId, status, from);
        
        return Ok(orders.Adapt<IReadOnlyCollection<OrderResponse>>());
    }
}