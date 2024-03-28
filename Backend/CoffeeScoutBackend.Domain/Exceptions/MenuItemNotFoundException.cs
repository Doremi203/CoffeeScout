namespace CoffeeScoutBackend.Domain.Exceptions;

public class MenuItemNotFoundException : NotFoundException
{
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

    public long? Id { get; init; }
}