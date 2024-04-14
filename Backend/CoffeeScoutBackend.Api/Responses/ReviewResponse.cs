namespace CoffeeScoutBackend.Api.Responses;

public record ReviewResponse
{
    public required long Id { get; init; }
    public required string Content { get; init; }
    public required int Rating { get; init; }
    public required CustomerInfoResponse Customer { get; init; }
}