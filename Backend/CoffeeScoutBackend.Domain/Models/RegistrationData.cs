namespace CoffeeScoutBackend.Domain.Models;

public record RegistrationData(
    string FirstName,
    string Email,
    string Password
);