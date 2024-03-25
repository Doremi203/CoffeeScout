namespace CoffeeScoutBackend.Domain.Models;

public record User
{
    public long Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public Role Role { get; init; }
}