namespace CoffeeScoutBackend.Domain.Models;

public record PaginationModel
{
    public required int PageSize { get; init; }
    public required int PageNumber { get; init; }
}