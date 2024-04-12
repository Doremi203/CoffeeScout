namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record AddCafeRequest(
    string Name,
    double Latitude,
    double Longitude,
    string Address,
    long CoffeeChainId,
    IReadOnlyCollection<AddCafeRequest.WorkingHoursRequest> WorkingHours
)
{
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
}