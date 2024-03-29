namespace CoffeeScoutBackend.Api.Requests;

public record CustomerRegisterRequest(
    string FirstName,
    string Email,
    string Password
);