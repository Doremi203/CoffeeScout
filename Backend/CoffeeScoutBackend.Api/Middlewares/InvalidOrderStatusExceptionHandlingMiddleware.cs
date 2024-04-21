using CoffeeScoutBackend.Domain.Exceptions;

namespace CoffeeScoutBackend.Api.Middlewares;

public class InvalidOrderStatusExceptionHandlingMiddleware(
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
        catch (InvalidOrderStatusException e)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails =
                {
                    Title = "Order is not in required status",
                    Detail = e.Message,
                    Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.4"
                },
                Exception = e
            });
        }
    }
}