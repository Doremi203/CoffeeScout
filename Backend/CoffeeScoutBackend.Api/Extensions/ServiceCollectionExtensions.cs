using CoffeeScoutBackend.Api.Config;
using CoffeeScoutBackend.Api.Config.Swager;
using FluentValidation;
using MailerSendNetCore.Common.Extensions;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Swashbuckle.AspNetCore.Filters;

namespace CoffeeScoutBackend.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiSettings(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .Configure<AdminSettings>(
                configuration.GetSection(nameof(AdminSettings)))
            .Configure<MailerSendSettings>(
                configuration.GetSection(nameof(MailerSendSettings)));

        return services;
    }

    public static IServiceCollection AddApiInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMailerSendEmailClient(
            configuration.GetSection(nameof(MailerSendSettings)));

        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddFluentValidationAutoValidation();

        return services;
    }

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