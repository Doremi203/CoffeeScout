namespace CoffeeScoutBackend.Api.Requests;

public record AddCafeAdminRequest(
    string Email,
    string Password,
    long CafeId
);