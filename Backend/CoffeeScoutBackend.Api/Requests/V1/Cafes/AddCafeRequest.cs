using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record AddCafeRequest(
    string Name,
    double Latitude,
    double Longitude,
    string Address,
    long CoffeeChainId,
    IReadOnlyCollection<WorkingHours> WorkingHours
);