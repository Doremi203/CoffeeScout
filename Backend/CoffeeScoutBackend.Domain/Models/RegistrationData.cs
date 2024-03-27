namespace CoffeeScoutBackend.Domain.Models;

public record RegistrationData(
    string UserName,
    string Email,
    string Password
);