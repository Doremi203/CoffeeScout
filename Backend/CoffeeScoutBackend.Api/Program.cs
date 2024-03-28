using CoffeeScoutBackend.Api.Config;
using CoffeeScoutBackend.Api.Identity;
using CoffeeScoutBackend.Api.Middlewares;
using CoffeeScoutBackend.Bll;
using CoffeeScoutBackend.Dal;
using CoffeeScoutBackend.Dal.Entities;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<AdminSettings>(
        builder.Configuration.GetSection(nameof(AdminSettings)))
    .Configure<DatabaseSettings>(
        builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAuthentication();
builder.Services.AddAuthorizationBuilder();

var databaseSettings = builder.Configuration
    .GetRequiredSection(nameof(DatabaseSettings))
    .Get<DatabaseSettings>()!;

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGroup("api/v1/account").MapIdentityApi<AppUser>();

app.MapControllers();

await app.Services.SeedRolesAsync();
await app.Services.SeedSuperAdminAsync();

app.Run();