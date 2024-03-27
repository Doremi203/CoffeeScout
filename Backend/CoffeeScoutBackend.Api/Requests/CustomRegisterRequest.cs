namespace CoffeeScoutBackend.Api.Requests;

public record CustomRegisterRequest(
    string UserName,
    string Email,
    string Password
);