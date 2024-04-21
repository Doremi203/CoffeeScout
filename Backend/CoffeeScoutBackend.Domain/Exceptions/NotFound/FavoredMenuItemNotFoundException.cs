namespace CoffeeScoutBackend.Domain.Exceptions.NotFound;

public class FavoredMenuItemNotFoundException : NotFoundException
{
    public long? Id { get; init; }
    
    public FavoredMenuItemNotFoundException(long id)
    {
        Id = id;
    }

    public FavoredMenuItemNotFoundException(string? message, long? id) 
        : this(message, id, null)
    {
        Id = id;
    }

    public FavoredMenuItemNotFoundException(string? message, long? id, Exception? innerException) : base(message, innerException)
    {
        Id = id;
    }
}