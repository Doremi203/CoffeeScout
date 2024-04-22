namespace CoffeeScoutBackend.Api.Requests.V1.Cafes;

public record TimeRequest(
    int Hour,
    int Minute
);