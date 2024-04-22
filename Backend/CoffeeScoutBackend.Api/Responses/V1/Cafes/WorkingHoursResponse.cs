namespace CoffeeScoutBackend.Api.Responses.V1.Cafes;

public record WorkingHoursResponse
{
    public required DayOfWeek DayOfWeek { get; set; }
    public required TimeResponse OpeningTime { get; set; }
    public required TimeResponse ClosingTime { get; set; }

    public record TimeResponse
    {
        public required int Hour { get; set; }
        public required int Minute { get; set; }
    }
}