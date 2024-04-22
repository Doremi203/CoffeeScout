namespace CoffeeScoutBackend.Domain.Exceptions.NotFound;

public class ReviewNotFoundException : NotFoundException
{
    public long? Id { get; init; }
    
    public ReviewNotFoundException(long? id)
    {
        Id = id;
    }

    public ReviewNotFoundException(string? message, long? id) 
        : this(message, id, null)
    {
    }

    public ReviewNotFoundException(string? message, long? id, Exception? innerException) : base(message, innerException)
    {
        Id = id;
    }
}