using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses;

public class CafeResponse
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required Location Location { get; set; }
    public required string Address { get; set; }
    public required IReadOnlyCollection<WorkingHoursResponse> WorkingHours { get; set; }

    public record WorkingHoursResponse
    {
        public required DayOfWeek Day { get; set; }
        public required TimeResponse OpeningTime { get; set; }
        public required TimeResponse ClosingTime { get; set; }

        public record TimeResponse
        {
            public required int Hour { get; set; }
            public required int Minute { get; set; }
        }
    }
}