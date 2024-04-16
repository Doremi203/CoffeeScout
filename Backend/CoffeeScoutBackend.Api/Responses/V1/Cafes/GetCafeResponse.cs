using CoffeeScoutBackend.Api.Responses.V1.CoffeeChains;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Api.Responses.V1.Cafes;

public class GetCafeResponse
{
    public required long Id { get; set; }
    public required GetCoffeeChainResponse CoffeeChain { get; set; }
    public required string Name { get; set; }
    public required Location Location { get; set; }
    public required string Address { get; set; }
    public required IReadOnlyCollection<WorkingHoursResponse> WorkingHours { get; set; }
}