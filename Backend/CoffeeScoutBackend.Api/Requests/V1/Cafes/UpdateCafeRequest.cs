using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record UpdateCafeRequest(
    string Name,
    Location Location,
    string Address,
    WorkingHoursRequest[] WorkingHours
);