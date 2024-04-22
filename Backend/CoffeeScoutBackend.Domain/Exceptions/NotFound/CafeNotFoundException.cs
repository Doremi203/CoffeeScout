namespace CoffeeScoutBackend.Domain.Exceptions.NotFound;

public class CafeNotFoundException : NotFoundException
{
    public long? Id { get; init; }
    
    public CafeNotFoundException(long id)
    {
        Id = id;
    }

    public CafeNotFoundException(string? message, long? id = null)
        : this(message, id, null)
    {
    }

    public CafeNotFoundException(string? message, long? id, Exception? innerException) : base(message,
        innerException)
    {
        Id = id;
    }
}