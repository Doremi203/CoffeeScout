namespace CoffeeScoutBackend.Domain.Models;

public record WorkingHours
{
    public long Id { get; init; }
    public DayOfWeek DayOfWeek { get; init; }
    public TimeOnly OpeningTime { get; init; }
    public TimeOnly ClosingTime { get; init; }
    public required Cafe Cafe { get; init; }
}