namespace CoffeeScoutBackend.Domain.Exceptions;

public class MenuItemAlreadyFavoredException(
    string? s,
    long menuItemId) : Exception(s)
{
    public long MenuItemId { get; set; } = menuItemId;
}