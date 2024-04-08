namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record UpdateCafeRequest(
    string Name,
    double Latitude,
    double Longitude
);