using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CoffeeScoutBackend.IntegrationTests.Infrastructure;

public class TestAuthMiddleware(
    RequestDelegate next
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userRole = context.Request.Headers["X-Test-UserRole"].FirstOrDefault();
        var userEmail = context.Request.Headers["X-Test-UserEmail"].FirstOrDefault();

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, userEmail!),
            new(ClaimTypes.Role, userRole!)
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);

        context.User = principal;

        await next(context);
    }
}