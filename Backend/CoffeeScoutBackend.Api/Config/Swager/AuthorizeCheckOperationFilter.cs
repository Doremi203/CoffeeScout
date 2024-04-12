using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoffeeScoutBackend.Api.Config.Swager;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var roles = context.MethodInfo.
            GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Select(a => a.Roles)
            .Distinct()
            .ToArray();

        if(!roles.Any())
        {
            roles = context.MethodInfo.DeclaringType?
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.Roles)
                .Distinct()
                .ToArray();
        }
        
        if (roles.Any())
        {
            string rolesStr = string.Join(", ", roles);
            // we can choose summary or description as per our preference
            operation.Description += $"<p> Available to Roles {rolesStr}</p>";
        }
    }
}