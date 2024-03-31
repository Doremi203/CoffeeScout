using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/beverage-types")]
public class BeverageTypeController(
    IMenuItemService menuItemService
) : ControllerBase
{
    [HttpPost("beverage-types")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddBeverageTypeAsync(string name)
    {
        var newBeverageType = new BeverageType
        {
            Name = name
        };
        await menuItemService.AddBeverageTypeAsync(newBeverageType);

        return Created();
    }
}