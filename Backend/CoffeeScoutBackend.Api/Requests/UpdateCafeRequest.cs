namespace CoffeeScoutBackend.Api.Requests;

public record UpdateCafeRequest(
    string Name,
    double Latitude,
    double Longitude
);