using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces.Services;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountsController(
    ICustomerService customerService,
    ISuperAdminService superAdminService
) : ControllerBase
{
    [HttpPost("customer/register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterCustomer(CustomerRegisterRequest request)
    {
        try
        {
            await customerService.RegisterCustomer(
                request.Adapt<CustomerRegistrationData>());
            return Ok();
        }
        catch (RegistrationException e)
        {
            var validationProblemDetails = new ValidationProblemDetails(e.RegistrationErrors)
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred."
            };

            return BadRequest(validationProblemDetails);
        }
    }
    
    [HttpPost("cafe-admin/register")]
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddCafeAdmin(AddCafeAdminRequest request)
    {
        await superAdminService.AddCafeAdmin(
            request.Adapt<CafeAdminRegistrationData>());

        return Ok();
    }
}