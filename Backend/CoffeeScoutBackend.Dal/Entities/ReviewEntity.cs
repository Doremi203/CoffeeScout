namespace CoffeeScoutBackend.Dal.Entities;

public record ReviewEntity
{
    public long Id { get; init; }
    public string CustomerId { get; init; } = string.Empty;
    public long MenuItemId { get; init; }
    public required string Content { get; init; }
    public int Rating { get; init; }
    
    public required CustomerEntity Customer { get; init; }
    public required MenuItemEntity MenuItem { get; init; }
}