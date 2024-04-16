using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record AddCafeRequest(
    string Name,
    Location Location,
    string Address,
    long CoffeeChainId,
    WorkingHoursRequest[] WorkingHours
);