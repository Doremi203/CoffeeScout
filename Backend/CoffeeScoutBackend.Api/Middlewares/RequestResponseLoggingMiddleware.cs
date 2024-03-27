namespace CoffeeScoutBackend.Api.Middlewares;

public class RequestResponseLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestResponseLoggingMiddleware> logger
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);
        var method = context.Request.Method;
        var path = context.Request.Path;
        var queryString = context.Request.QueryString;
        logger.LogInformation($"Request: {method} {path} {queryString}");
        
        var code = context.Response.StatusCode;
        logger.LogInformation($"Response: {code}");
    }
}