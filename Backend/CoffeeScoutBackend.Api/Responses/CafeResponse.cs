using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses;

public class CafeResponse
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required Location Location { get; set; }
}