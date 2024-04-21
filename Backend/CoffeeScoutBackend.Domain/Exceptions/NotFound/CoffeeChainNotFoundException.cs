namespace CoffeeScoutBackend.Domain.Exceptions.NotFound;

public class CoffeeChainNotFoundException : NotFoundException
{
    public long? Id { get; init; }
    
    public CoffeeChainNotFoundException(long? id)
    {
        Id = id;
    }

    public CoffeeChainNotFoundException(string? message, long? id) 
        : this(message, id, null)
    {
    }

    public CoffeeChainNotFoundException(string? message, long? id, Exception? innerException) : base(message, innerException)
    {
        Id = id;
    }
}