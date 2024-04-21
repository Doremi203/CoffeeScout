using System.Security.Claims;
using CoffeeScoutBackend.Domain.Exceptions;
using CoffeeScoutBackend.Domain.Exceptions.NotFound;

namespace CoffeeScoutBackend.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)
               ?? throw new UserNotFoundException("User ID not found");
    }
}