namespace CoffeeScoutBackend.Domain.Exceptions.NotFound;

public class MenuItemNotFoundException : NotFoundException
{
    public long? Id { get; init; }
    
    public MenuItemNotFoundException(long id)
    {
        Id = id;
    }

    public MenuItemNotFoundException(string? message, long? id)
        : this(message, id, null)
    {
    }

    public MenuItemNotFoundException(string? message, long? id, Exception? innerException) : base(message,
        innerException)
    {
        Id = id;
    }
}