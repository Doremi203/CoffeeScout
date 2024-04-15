namespace CoffeeScoutBackend.Domain.Exceptions;

public class WorkingHoursNotFoundException : NotFoundException
{
    public DayOfWeek? DayOfWeek { get; init; }

    public WorkingHoursNotFoundException(DayOfWeek? dayOfWeek)
    {
        DayOfWeek = dayOfWeek;
    }

    public WorkingHoursNotFoundException(string? message, DayOfWeek? dayOfWeek)
        : this(message, dayOfWeek, null)
    {
    }

    public WorkingHoursNotFoundException(string? message, DayOfWeek? dayOfWeek, Exception? innerException) : base(message,
        innerException)
    {
        DayOfWeek = dayOfWeek;
    }
}