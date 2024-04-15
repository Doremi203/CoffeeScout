namespace CoffeeScoutBackend.Domain.Exceptions;

public class WorkingHoursNotFoundException : NotFoundException
{
    public long? Id { get; init; }

    public WorkingHoursNotFoundException(long? id)
    {
        Id = id;
    }

    public WorkingHoursNotFoundException(string? message, long? id)
        : this(message, id, null)
    {
    }

    public WorkingHoursNotFoundException(string? message, long? id, Exception? innerException) : base(message,
        innerException)
    {
        Id = id;
    }
}