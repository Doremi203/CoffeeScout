using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record GetCafeOrdersRequest(
    OrderStatus Status,
    DateTime From
);