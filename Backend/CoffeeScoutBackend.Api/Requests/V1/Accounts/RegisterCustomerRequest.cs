namespace CoffeeScoutBackend.Api.Requests.V1.Accounts;

public record RegisterCustomerRequest(
    string FirstName,
    string Email,
    string Password
);