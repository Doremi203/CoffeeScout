namespace CoffeeScoutBackend.Dal.Entities;

public record ReviewEntity
{
    public long Id { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public long MenuItemId { get; set; }
    public required string Content { get; set; }
    public int Rating { get; set; }
    
    public required CustomerEntity Customer { get; set; }
    public required MenuItemEntity MenuItem { get; set; }
}