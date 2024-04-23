using CoffeeScoutBackend.Api;
using CoffeeScoutBackend.Api.DbSeeders;
using CoffeeScoutBackend.Api.Extensions;
using CoffeeScoutBackend.Api.Requests.Mappers;
using CoffeeScoutBackend.Api.Responses.Mappers;
using CoffeeScoutBackend.Bll.Extensions;
using CoffeeScoutBackend.Dal;
using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Dal.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ResponseMapperConfiguration.Configure();
RequestMapperConfiguration.Configure();
builder.Services.AddApiSettings(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

builder.Services.AddAuthentication();
builder.Services.AddAuthorizationBuilder();

builder.Services.AddTransient<IDbSeeder, ProdDbSeeder>();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddTransient<IDbSeeder, TestDbSeeder>();
    builder.Services.AddHttpLogging(_ => { });
}

builder.Services
    .AddIdentityServices()
    .AddBllServices()
    .AddDalServices(builder.Configuration);

builder.Services.AddApiInfrastructure(builder.Configuration);
builder.Services.AddValidators();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

if (app.Environment.IsDevelopment())
{
    await dbContext.Database.EnsureDeletedAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

if (app.Environment.IsEnvironment("ApiTests")) await dbContext.Database.EnsureDeletedAsync();

//app.UseHttpsRedirection();

app
    .MapGroup(RoutesV1.Accounts)
    .WithTags("Accounts")
    .MapIdentityApi<AppUser>();
// map health check endpoint
app.MapHealthChecks("/api/v1/health");

app.UseAuthorization();

app.AddExceptionHandlingMiddlewares();

app.MapControllers();

await dbContext.Database.MigrateAsync();
var dbSeeder = app.Services.GetRequiredService<IDbSeeder>();
await dbSeeder.SeedDbAsync();

app.Run();