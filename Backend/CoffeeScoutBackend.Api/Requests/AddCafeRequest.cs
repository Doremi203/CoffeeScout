namespace CoffeeScoutBackend.Api.Requests;

public record AddCafeRequest(
    string Name,
    double Latitude,
    double Longitude
);