namespace CoffeeScoutBackend.Domain.Models;

public record CustomerRegistrationData(
    string FirstName,
    string Email,
    string Password
);