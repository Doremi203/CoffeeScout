using CoffeeScoutBackend.Api.Requests.V1.Accounts;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.Domain.ServiceModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route(RoutesV1.Accounts)]
public class AccountsController(
    ICustomerService customerService,
    ICafeAdminService cafeAdminService
) : ControllerBase
{
    [HttpPost("customer/register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterCustomer(RegisterCustomerRequest request)
    {
        await customerService.RegisterCustomer(
            request.Adapt<CustomerRegistrationData>());

        return Ok();
    }

    [HttpPost("cafe-admin/register")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterCafeAdmin(RegisterCafeAdminRequest request)
    {
        await cafeAdminService.AddCafeAdmin(
            request.Adapt<CafeAdminRegistrationData>());

        return Ok();
    }
}