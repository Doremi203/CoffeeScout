using System.Net;
using System.Transactions;
using CoffeeScoutBackend.Api.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController(
    UserManager<AppUser> userManager
) : ControllerBase
{
    [HttpPost("customRegister")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var user = new AppUser { UserName = model.Email, Email = model.Email };
        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var roleResult = await userManager.AddToRoleAsync(user, "User");

            if (roleResult.Succeeded)
            {
                scope.Complete();
                return Ok();
            }

            AddErrors(roleResult);
        }
        AddErrors(result);
        
        var validationProblemDetails = new ValidationProblemDetails(ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Status = StatusCodes.Status400BadRequest,
            Title = "One or more validation errors occurred."
        };

        return BadRequest(validationProblemDetails);
    }

    private void AddErrors(IdentityResult roleResult)
    {
        foreach (var error in roleResult.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
    }
}