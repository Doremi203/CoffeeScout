using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = nameof(Roles.CafeAdmin))]
public class CafeController(
    ICafeService cafeService
) : ControllerBase
{
    [HttpPost("menu-items")]
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
        await cafeService.AddMenuItemAsync(newMenuItem);
        return Ok();
    }
}