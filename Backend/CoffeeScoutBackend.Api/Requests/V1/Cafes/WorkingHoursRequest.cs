namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record WorkingHoursRequest(
    DayOfWeek DayOfWeek,
    TimeRequest OpeningTime,
    TimeRequest ClosingTime
);