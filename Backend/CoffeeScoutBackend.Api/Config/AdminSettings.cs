namespace CoffeeScoutBackend.Api.Config;

public record AdminSettings
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}