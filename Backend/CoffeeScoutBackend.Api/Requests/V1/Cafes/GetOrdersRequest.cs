using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record GetOrdersRequest(
    OrderStatus Status,
    int PageSize,
    int PageNumber
);