using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record GetCafesRequest(
    Location Location,
    double RadiusInMeters
);