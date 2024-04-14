namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record WorkingHoursRequest(
    DayOfWeek Day,
    WorkingHoursRequest.TimeRequest OpeningTime,
    WorkingHoursRequest.TimeRequest ClosingTime
)
{
    public record TimeRequest(
        int Hour,
        int Minute
    );
}