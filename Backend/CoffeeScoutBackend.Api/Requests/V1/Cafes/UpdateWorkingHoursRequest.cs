namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record UpdateWorkingHoursRequest(
    long Id,
    TimeRequest OpeningTime,
    TimeRequest ClosingTime
);