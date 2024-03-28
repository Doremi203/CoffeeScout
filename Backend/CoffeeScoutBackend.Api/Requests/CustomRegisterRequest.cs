namespace CoffeeScoutBackend.Api.Requests;

public record CustomRegisterRequest(
    string FirstName,
    string Email,
    string Password
);