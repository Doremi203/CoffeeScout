namespace CoffeeScoutBackend.Domain.Exceptions;

public class MenuItemNotFavoredException : NotFoundException
{
    public long? Id { get; init; }
    
    public MenuItemNotFavoredException(long id)
    {
        Id = id;
    }

    public MenuItemNotFavoredException(string? message, long? id) 
        : this(message, id, null)
    {
        Id = id;
    }

    public MenuItemNotFavoredException(string? message, long? id, Exception? innerException) : base(message, innerException)
    {
        Id = id;
    }
}