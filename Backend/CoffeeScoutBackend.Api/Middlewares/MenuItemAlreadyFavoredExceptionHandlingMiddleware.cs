using CoffeeScoutBackend.Domain.Exceptions;

namespace CoffeeScoutBackend.Api.Middlewares;

public class MenuItemAlreadyFavoredExceptionHandlingMiddleware(
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
        catch (MenuItemAlreadyFavoredException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails =
                {
                    Title = "Menu item already favored",
                    Detail = ex.Message,
                    Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
                },
                Exception = ex
            });
        }
    }
}