namespace CoffeeScoutBackend.Domain.ServiceModels;

public record CafeAdminRegistrationData(
    string Email,
    string Password,
    long CafeId
);