namespace CoffeeScoutBackend.Api.Responses.V1.MenuItems;

public record AddReviewResponse
{
    public required long Id { get; init; }
    public required string Content { get; init; }
    public required int Rating { get; init; }
    public required string CustomerId { get; init; }
}