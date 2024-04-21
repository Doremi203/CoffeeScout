using CoffeeScoutBackend.Domain.Exceptions.NotFound;

namespace CoffeeScoutBackend.Api.Middlewares;

public class NotFoundExceptionHandlingMiddleware(
    RequestDelegate next,
    IProblemDetailsService problemDetailsService)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails =
                {
                    Title = "Resource not found",
                    Detail = e.Message,
                    Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.4"
                },
                Exception = e
            });
        }
    }
}