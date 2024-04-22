namespace CoffeeScoutBackend.Api.Responses.V1.Customers;

public record GetCustomerInfoResponse
{
    public required string FirstName { get; init; }
}