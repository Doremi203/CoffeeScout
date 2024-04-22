using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.Domain.ServiceModels;

namespace CoffeeScoutBackend.Api.Requests.V1.Orders;

public record GetOrdersRequest(
    OrderStatus Status,
    PaginationModel Pagination
);