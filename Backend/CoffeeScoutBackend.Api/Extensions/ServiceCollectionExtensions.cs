using CoffeeScoutBackend.Api.Config.Swager;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CoffeeScoutBackend.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.OperationFilter<AuthorizeCheckOperationFilter>();

            options.DocumentFilter<RemovePathsDocumentFilter>();
        });
        
        return services;
    }
}