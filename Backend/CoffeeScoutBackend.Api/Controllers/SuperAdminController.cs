using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = nameof(Roles.SuperAdmin))]
public class SuperAdminController(
    ISuperAdminService superAdminService
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
        await superAdminService.AddBeverageTypeAsync(newBeverageType);
        return Ok();
    }
}