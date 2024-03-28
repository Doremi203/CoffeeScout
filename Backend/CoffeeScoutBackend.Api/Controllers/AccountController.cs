using CoffeeScoutBackend.Api.Requests;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Interfaces;
using CoffeeScoutBackend.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController(
    ICustomerService customerService
) : ControllerBase
{
    [HttpPost("customRegister")]
    public async Task<IActionResult> Register(CustomRegisterRequest request)
    {
        try
        {
            await customerService.RegisterCustomerAsync(
                request.Adapt<RegistrationData>());
            return Ok();
        }
        catch (CustomerRegistrationException e)
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
}