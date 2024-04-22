namespace CoffeeScoutBackend.Domain.ServiceModels;

public record PaginationModel
{
    public required int PageSize { get; init; }
    public required int PageNumber { get; init; }
}