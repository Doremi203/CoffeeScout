using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = nameof(Roles.SuperAdmin))]
public class SuperAdminController(
    ISuperAdminService superAdminService,
    IMenuItemService menuItemService
) : ControllerBase
{
    [HttpPost("cafe-admins")]
    public async Task<IActionResult> AddCafeAdminAsync(AddCafeAdminRequest request)
    {
        await superAdminService.AddCafeAdminAsync(
            request.Adapt<CafeAdminRegistrationData>());

        return Ok();
    }

    [HttpPost("beverage-types")]
    public async Task<IActionResult> AddBeverageTypeAsync(string name)
    {
        var newBeverageType = new BeverageType
        {
            Name = name
        };
        await menuItemService.AddBeverageTypeAsync(newBeverageType);

        return Ok();
    }
}