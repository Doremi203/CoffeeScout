using CoffeeScoutBackend.Domain.Exceptions;

namespace CoffeeScoutBackend.Api.Middlewares;

public class PaymentExceptionHandlingMiddleware(
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
        catch (PaymentException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails =
                {
                    Title = "Payment error",
                    Detail = ex.Message,
                    Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1"
                },
                Exception = ex
            });
        }
    }
}