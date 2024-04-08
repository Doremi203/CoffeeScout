namespace CoffeeScoutBackend.Domain.Models;

public record Review
{
    public long Id { get; init; }
    public long CustomerId { get; init; }
    public long MenuItemId { get; init; }
    public required string Content { get; init; }
    public int Rating { get; init; }
    
    public Customer Customer { get; init; }
    public MenuItem MenuItem { get; init; }
}