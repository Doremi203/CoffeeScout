namespace CoffeeScoutBackend.Api.Responses;

public record ReviewResponse
{
    public required long Id { get; init; }
    public required string Content { get; init; }
    public required int Rating { get; init; }
    public required string CustomerId { get; init; }
    public required long MenuItemId { get; init; }
}