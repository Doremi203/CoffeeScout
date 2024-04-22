namespace CoffeeScoutBackend.Api.Requests.V1.Accounts;

public record RegisterCafeAdminRequest(
    string Email,
    string Password,
    long CafeId
);