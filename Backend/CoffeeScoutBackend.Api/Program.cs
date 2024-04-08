using CoffeeScoutBackend.Api.Config;
using CoffeeScoutBackend.Api.DbSeeders;
using CoffeeScoutBackend.Api.Identity;
using CoffeeScoutBackend.Api.Middlewares;
using CoffeeScoutBackend.Bll;
using CoffeeScoutBackend.Dal;
using CoffeeScoutBackend.Dal.Config;
using CoffeeScoutBackend.Dal.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<AdminSettings>(
        builder.Configuration.GetSection(nameof(AdminSettings)))
    .Configure<DatabaseSettings>(
        builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        options.OperationFilter<SecurityRequirementsOperationFilter>();

        options.DocumentFilter<RemovePathsDocumentFilter>();
    }
);
builder.Services.AddControllers();

builder.Services.AddAuthentication();
builder.Services.AddAuthorizationBuilder();

var databaseSettings = builder.Configuration
    .GetRequiredSection(nameof(DatabaseSettings))
    .Get<DatabaseSettings>()!;

builder.Services.AddTransient<IDbSeeder, TestDbSeeder>();
builder.Services
    .AddIdentityServices()
    .AddBllServices()
    .AddDalServices(databaseSettings);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseMiddleware<RequestResponseLoggingMiddleware>();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.MigrateAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app
    .MapGroup("api/v1/accounts")
    .WithTags("Accounts")
    .MapIdentityApi<AppUser>();

app.UseAuthorization();

app.MapControllers();

var dbSeeder = app.Services.GetRequiredService<IDbSeeder>();
await dbSeeder.SeedDbAsync();

app.Run();