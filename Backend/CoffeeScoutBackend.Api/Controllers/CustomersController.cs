using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/customers")]
[Authorize(Roles = nameof(Roles.Customer))]
public class CustomersController(
    ICustomerService customerService
) : ControllerBase
{
    [HttpPost("favored-menu-items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddFavoredMenuItemAsync(long menuItemId)
    {
        await customerService.AddFavoredMenuItemAsync(User.GetId(), menuItemId);
        return Created($"api/v1/customers/favored-menu-items/{menuItemId}", null);
    }

    [HttpGet("favored-beverage-types")]
    [ProducesResponseType<IReadOnlyCollection<BeverageType>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFavoredBeverageTypesAsync()
    {
        var favoredBeverageTypes =
            await customerService.GetFavoredBeverageTypesAsync(User.GetId());
        return Ok(favoredBeverageTypes);
    }
}