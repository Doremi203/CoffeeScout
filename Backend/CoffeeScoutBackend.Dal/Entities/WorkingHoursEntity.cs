namespace CoffeeScoutBackend.Dal.Entities;

public record WorkingHoursEntity
{
    public long Id { get; set; }
    public long CafeId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }

    public CafeEntity Cafe { get; set; }
}