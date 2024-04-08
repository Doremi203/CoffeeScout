namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record GetCafesRequest(
    double Latitude,
    double Longitude,
    double RadiusInMeters
);