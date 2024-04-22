namespace CoffeeScoutBackend.Domain.ServiceModels;

public record CustomerRegistrationData(
    string FirstName,
    string Email,
    string Password
);