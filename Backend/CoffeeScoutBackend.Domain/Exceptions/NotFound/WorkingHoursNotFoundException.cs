namespace CoffeeScoutBackend.Domain.Exceptions.NotFound;

public class WorkingHoursNotFoundException : NotFoundException
{
    public DayOfWeek? Id { get; init; }

    public WorkingHoursNotFoundException(DayOfWeek? id)
    {
        Id = id;
    }

    public WorkingHoursNotFoundException(string? message, DayOfWeek? id)
        : this(message, id, null)
    {
    }

    public WorkingHoursNotFoundException(string? message, DayOfWeek? id, Exception? innerException) : base(message,
        innerException)
    {
        Id = id;
    }
}