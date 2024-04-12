using CoffeeScoutBackend.Domain.Models;

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
    public class WorkingHoursRequest
    {
        public record TimeRequest
        {
            public int Hour { get; init; }
            public int Minute { get; init; }
        }
        public DayOfWeek Day { get; init; }
        public TimeRequest OpeningTime { get; init; }
        public TimeRequest ClosingTime { get; init; }
    }
}