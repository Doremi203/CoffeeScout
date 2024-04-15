namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record WorkingHoursRequest(
    DayOfWeek Day,
    TimeRequest OpeningTime,
    TimeRequest ClosingTime
);