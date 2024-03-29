namespace CoffeeScoutBackend.Domain.Models;

public record CafeAdminRegistrationData(
    string Email,
    string Password,
    long CafeId
);