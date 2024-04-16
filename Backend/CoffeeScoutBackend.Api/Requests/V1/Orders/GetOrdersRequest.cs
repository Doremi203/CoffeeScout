using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Requests.V1.Orders;

public record GetOrdersRequest(
    OrderStatus Status,
    PaginationModel Pagination
);