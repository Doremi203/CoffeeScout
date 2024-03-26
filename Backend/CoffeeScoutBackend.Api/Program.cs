using CoffeeScoutBackend.Api.Config;
using CoffeeScoutBackend.Api.Identity;
using CoffeeScoutBackend.Dal;

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

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
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