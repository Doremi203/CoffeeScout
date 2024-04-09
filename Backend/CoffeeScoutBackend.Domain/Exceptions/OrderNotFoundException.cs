namespace CoffeeScoutBackend.Domain.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public long? Id { get; init; }

    public OrderNotFoundException(long? id)
    {
        Id = id;
    }

    public OrderNotFoundException(string? message, long? id)
        : this(message, id, null)
    {
    }

    public OrderNotFoundException(string? message, long? id, Exception? innerException) : base(message, innerException)
    {
        Id = id;
    }
}