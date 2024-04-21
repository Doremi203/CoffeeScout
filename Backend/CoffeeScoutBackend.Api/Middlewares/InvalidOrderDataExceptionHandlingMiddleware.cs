using CoffeeScoutBackend.Domain.Exceptions;

namespace CoffeeScoutBackend.Api.Middlewares;

public class InvalidOrderDataExceptionHandlingMiddleware(
    RequestDelegate next,
    IProblemDetailsService problemDetailsService
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (InvalidOrderDataException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails =
                {
                    Title = "Invalid order data",
                    Detail = e.Message,
                    Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.4"
                },
                Exception = e
            });
        }
    }
}