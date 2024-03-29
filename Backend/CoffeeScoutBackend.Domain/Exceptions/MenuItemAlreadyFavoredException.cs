namespace CoffeeScoutBackend.Domain.Exceptions;

public class MenuItemAlreadyFavoredException(
    string? s,
    long menuItemId,
    string currentUserId) : Exception(s)
{
    public long MenuItemId { get; set; } = menuItemId;
    public string CurrentUserId { get; set; } = currentUserId;
}