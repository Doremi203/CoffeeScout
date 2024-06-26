using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoffeeScoutBackend.Api.Config.Swager;

public class RemovePathsDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathsToRemove = new List<string>
        {
            "/api/v1/accounts/manage/2fa",
            "/api/v1/accounts/register"
        };

        foreach (var path in pathsToRemove) swaggerDoc.Paths.Remove(path);
    }
}