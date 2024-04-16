using CoffeeScoutBackend.Api.Responses.V1.Customers;

namespace CoffeeScoutBackend.Api.Responses.V1.MenuItems;

public record GetReviewResponse
{
    public required long Id { get; init; }
    public required string Content { get; init; }
    public required int Rating { get; init; }
    public required GetCustomerInfoResponse Customer { get; init; }
}