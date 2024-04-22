using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.ServiceModels;

public record GetOrdersModel
{
    public required OrderStatus Status { get; init; }
    public required int PageSize { get; init; }
    public required int PageNumber { get; init; }
}