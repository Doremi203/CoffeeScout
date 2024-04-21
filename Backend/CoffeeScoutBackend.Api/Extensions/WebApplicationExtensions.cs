using CoffeeScoutBackend.Api.Middlewares;

namespace CoffeeScoutBackend.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication AddExceptionHandlingMiddlewares(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseMiddleware<NotFoundExceptionHandlingMiddleware>();
        app.UseMiddleware<InvalidOrderDataExceptionHandlingMiddleware>();
        app.UseMiddleware<InvalidOrderStatusExceptionHandlingMiddleware>();
        app.UseMiddleware<PaymentExceptionHandlingMiddleware>();
        app.UseMiddleware<RegistrationExceptionHandlingMiddleware>();
        return app;
    }
}