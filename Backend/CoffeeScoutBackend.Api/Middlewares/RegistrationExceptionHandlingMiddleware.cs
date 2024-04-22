using CoffeeScoutBackend.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeScoutBackend.Api.Middlewares;

public class RegistrationExceptionHandlingMiddleware(
    RequestDelegate next
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (RegistrationException e)
        {
            var validationProblemDetails = new ValidationProblemDetails(e.RegistrationErrors)
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred."
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(validationProblemDetails);
        }
    }
}